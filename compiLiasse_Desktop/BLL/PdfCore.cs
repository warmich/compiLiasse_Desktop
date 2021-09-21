using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;

using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

using static compiLiasse_Desktop.BLL.Utilities;

namespace compiLiasse_Desktop.BLL
{
	class PdfCore
	{
		static string titre = "Test d'un titre en entrant une variable";
		internal static void MergePdfs(List<FilePdf> pPdfList)
		{
			PdfDocument outputPdf = new PdfDocument();

			// Boucle sur les fichiers pdf
			foreach (var item in pPdfList)
			{
				string itemPath = Path.Combine(item.FilePath, item.FileName);
				PdfDocument inputPdf = PdfReader.Open(itemPath, PdfDocumentOpenMode.Import);

				// Boucle sur les pages
				int count = inputPdf.PageCount;
				for (int num = 0; num < count; num++)
				{
					// Prend la page du PDF à importer
					PdfPage page = inputPdf.Pages[num];
					// Ajoute la page au pdf compilé
					outputPdf.AddPage(page);
				}
			}
			outputPdf.Save(Path.Combine(usersPath, "mergedFile.pdf"));

			// Démarrer le viewer PDF
			//Process.Start(filename);
		}

		internal static void NewPdf()
		{
			// Create a new PDF document
			PdfDocument document = new PdfDocument();
			document.Info.Title = "Created with PDFsharp";

			// Create an empty page
			PdfPage page = document.AddPage();

			// Get an XGraphics object for drawing
			XGraphics gfx = XGraphics.FromPdfPage(page);

			// Create a font
			XFont font = new XFont("Verdana", 20, XFontStyle.BoldItalic);

			// Draw the text
			gfx.DrawString("Hello, World!", font, XBrushes.Black,
			  new XRect(0, 0, page.Width, page.Height),
			  XStringFormats.Center);

			// Save the document...
			document.Save(Path.Combine(pathBase, "filePdf1.pdf")); // Gérer l'erreur d'autorisation d'écriture sur le DD
																   // ...and start a viewer.
																   //Process.Start(filename);
		}

		internal static void creatingPdf()
		{

			DateTime now = DateTime.Now;
			string filename = "MixMigraDocAndPdfSharp.pdf";
			filename = Guid.NewGuid().ToString("D").ToUpper() + ".pdf";
			PdfDocument document = new PdfDocument();
			document.Info.Title = "PDFsharp XGraphic Sample";
			document.Info.Author = "Stefan Lange";
			document.Info.Subject = "Created with code snippets that show the use of graphical functions";
			document.Info.Keywords = "PDFsharp, XGraphics";

			SamplePage1(document);

			SamplePage2(document);

			Debug.WriteLine("seconds=" + (DateTime.Now - now).TotalSeconds.ToString());

			// Save the document...
			document.Save(Path.Combine(pathBase, filename));
		}

		static void SamplePage1(PdfDocument document)
		{
			PdfPage page = document.AddPage();
			XGraphics gfx = XGraphics.FromPdfPage(page);
			// HACK²
			gfx.MUH = PdfFontEncoding.Unicode;
			//gfx.MFEH = PdfFontEmbedding.Default;

			XFont font = new XFont("Verdana", 13, XFontStyle.Bold);

			gfx.DrawString(titre, font, XBrushes.Black,
			  new XRect(100, 100, page.Width - 200, 300), XStringFormats.Center);

			// You always need a MigraDoc document for rendering.
			Document doc = new Document();
			Section sec = doc.AddSection();
			// Add a single paragraph with some text and format information.
			Paragraph para = sec.AddParagraph();
			para.Format.Alignment = ParagraphAlignment.Justify;
			para.Format.Font.Name = "Times New Roman";
			para.Format.Font.Size = 12;
			para.Format.Font.Color = Colors.DarkGray;
			para.Format.Font.Color = Colors.DarkGray;
			para.AddText("Duisism odigna acipsum delesenisl ");
			para.AddFormattedText("ullum in velenit", TextFormat.Bold);
			para.AddText(" ipit iurero dolum zzriliquisis nit wis dolore vel et nonsequipit, velendigna " +
			  "auguercilit lor se dipisl duismod tatem zzrit at laore magna feummod oloborting ea con vel " +
			  "essit augiati onsequat luptat nos diatum vel ullum illummy nonsent nit ipis et nonsequis " +
			  "niation utpat. Odolobor augait et non etueril landre min ut ulla feugiam commodo lortie ex " +
			  "essent augait el ing eumsan hendre feugait prat augiatem amconul laoreet. ≤≥≈≠");
			para.Format.Borders.Distance = "5pt";
			para.Format.Borders.Color = Colors.Gold;

			// Create a renderer and prepare (=layout) the document
			DocumentRenderer docRenderer = new DocumentRenderer(doc);
			docRenderer.PrepareDocument();

			// Render the paragraph. You can render tables or shapes the same way.
			docRenderer.RenderObject(gfx, XUnit.FromCentimeter(5), XUnit.FromCentimeter(10), "12cm", para);
		}

		static void SamplePage2(PdfDocument document)
		{
			PdfPage page = document.AddPage();
			XGraphics gfx = XGraphics.FromPdfPage(page);
			// HACK²
			gfx.MUH = PdfFontEncoding.Unicode;
			//gfx.MFEH = PdfFontEmbedding.Default;

			// Create document from HalloMigraDoc sample
			Document doc = new();

			// Create a renderer and prepare (=layout) the document
			DocumentRenderer docRenderer = new DocumentRenderer(doc);
			docRenderer.PrepareDocument();

			// For clarity we use point as unit of measure in this sample.
			// A4 is the standard letter size in Germany (21cm x 29.7cm).
			XRect A4Rect = new XRect(0, 0, A4Width, A4Height);

			int pageCount = docRenderer.FormattedDocument.PageCount;
			for (int idx = 0; idx < pageCount; idx++)
			{
				XRect rect = GetRect(idx);

				// Use BeginContainer / EndContainer for simplicity only. You can naturaly use you own transformations.
				XGraphicsContainer container = gfx.BeginContainer(rect, A4Rect, XGraphicsUnit.Point);

				// Draw page border for better visual representation
				gfx.DrawRectangle(XPens.LightGray, A4Rect);

				// Render the page. Note that page numbers start with 1.
				docRenderer.RenderPage(gfx, idx + 1);

				// Note: The outline and the hyperlinks (table of content) does not work in the produced PDF document.

				// Pop the previous graphical state
				gfx.EndContainer(container);
			}
		}

		static XRect GetRect(int index)
		{
			XRect rect = new XRect(0, 0, A4Width / 3 * 0.9, A4Height / 3 * 0.9);
			rect.X = (index % 3) * A4Width / 3 + A4Width * 0.05 / 3;
			rect.Y = (index / 3) * A4Height / 3 + A4Height * 0.05 / 3;
			return rect;
		}
		static double A4Width = XUnit.FromCentimeter(21).Point;
		static double A4Height = XUnit.FromCentimeter(29.7).Point;
	}
}
