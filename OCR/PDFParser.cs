using PDFtoImage;
using SkiaSharp;
using Tesseract;

namespace CertiPurge.OCR;

class Converter
{
	public static async Task<string[]> ConvertPDFToText(byte[] bytesArr)
	{
		Console.WriteLine("Converting to images");

		using var pdfStream = new MemoryStream(bytesArr);

		var images = new List<SKBitmap>();
		await foreach (var img in Conversion.ToImagesAsync(pdfStream))
		{
			Console.WriteLine("Converted to image");
			images.Add(img);
		}

		Console.WriteLine($"Converted {images.Count} images.");

		var fullText = new List<string>();

		using var engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default);

		foreach (var img in images)
		{
			using var ms = new MemoryStream();
			img.Encode(SKEncodedImageFormat.Png, 100).SaveTo(ms);
			ms.Seek(0, SeekOrigin.Begin);
			using var pix = Pix.LoadFromMemory(ms.ToArray());
			using var page = engine.Process(pix);
			fullText.Add(page.GetWordStrBoxText(1));
		}

		return fullText.ToArray();
	}
}