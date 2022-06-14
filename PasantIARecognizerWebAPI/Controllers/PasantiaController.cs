using Azure;
using Azure.AI.FormRecognizer.DocumentAnalysis;
using Microsoft.AspNetCore.Mvc;
using PasantIARecognizerWebAPI.Models;

namespace PasantIARecognizerWebAPI.Controllers
{
    public class PasantiaController : Controller
    {
        private static readonly string endpoint = "https://jessformrecognizer.cognitiveservices.azure.com/";
        private static readonly string apiKey = "0bd31f6a8ad74efa95376576886179e4";
        private static readonly AzureKeyCredential credential = new AzureKeyCredential(apiKey);
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet(Name = "EjecutarPasantia")]
        public async Task<PasantiaResponse> EjecutarPasantIARecognizerAsync(String uriBlobStorage)
        {
            string modelId = "PasantIA";
            Uri fileUri = new Uri(uriBlobStorage);
            IDictionary<string, string> pasantia = new Dictionary<string, string>();

            var client = new DocumentAnalysisClient(new Uri(endpoint), credential);

            AnalyzeDocumentOperation operation = await client.StartAnalyzeDocumentFromUriAsync(modelId, fileUri);
            Response<AnalyzeResult> operationResponse = await operation.WaitForCompletionAsync();
            AnalyzeResult result = operationResponse.Value;

            //foreach (AnalyzedDocument document in result.Documents)
            //{
            //    Console.WriteLine($"Form of type: {form.FormType}");
            //    Console.WriteLine($"Form was analyzed with model with ID: {modelId}");
            //    foreach (FormField field in form.Fields.Values)
            //    {
            //        pasantia.Add(field.Name, field.ValueData.Text);
            //    }
            //}

            foreach (AnalyzedDocument document in result.Documents)
            {
                Console.WriteLine($"Document of type: {document.DocType}");

                foreach (KeyValuePair<string, DocumentField> fieldKvp in document.Fields)
                {
                    string fieldName = fieldKvp.Key;
                    DocumentField field = fieldKvp.Value;

                    Console.WriteLine($"Field '{fieldName}': ");

                    Console.WriteLine($"  Content: '{field.Content}'");
                    Console.WriteLine($"  Confidence: '{field.Confidence}'");
                }
            }

            foreach (KeyValuePair<string, string> dato in pasantia)
            {
                Console.WriteLine("{0}: {1}",
                dato.Key, dato.Value);
            }
        }
    }
}
