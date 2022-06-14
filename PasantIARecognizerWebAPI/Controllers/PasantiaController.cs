using Azure;
using Azure.AI.FormRecognizer.DocumentAnalysis;
using Microsoft.AspNetCore.Mvc;
using PasantIARecognizerWebAPI.Models;

namespace PasantIARecognizerWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class PasantiaController : Controller
    {
        private static readonly string endpoint = "https://jessformrecognizer.cognitiveservices.azure.com/";
        private static readonly string apiKey = "0bd31f6a8ad74efa95376576886179e4";
        private static readonly string modelId = "PasantIA";

        private static readonly AzureKeyCredential credential = new AzureKeyCredential(apiKey);

        [HttpGet(Name = "EjecutarPasantIARecognizer")]
        public async Task<PasantiaResponse> EjecutarPasantIARecognizerAsync(string uriBlobStorage)
        {
            Uri fileUri = new Uri(uriBlobStorage);
            IDictionary<string, string> pasantia = new Dictionary<string, string>();

            var client = new DocumentAnalysisClient(new Uri(endpoint), credential);

            AnalyzeDocumentOperation operation = await client.StartAnalyzeDocumentFromUriAsync(modelId, fileUri);
            Response<AnalyzeResult> operationResponse = await operation.WaitForCompletionAsync();
            AnalyzeResult result = operationResponse.Value;

            PasantiaResponse response = new PasantiaResponse();

            var a = result.Documents[0].Fields;

            response.Titulo = a.First(kvp => kvp.Key == "Titulo").Value.Content;
            response.Ubicacion = a.First(kvp => kvp.Key == "Ubicacion").Value.Content;
            response.Modalidad = a.First(kvp => kvp.Key == "Modalidad").Value.Content;
            response.Requisitos = a.First(kvp => kvp.Key == "Requisitos").Value.Content;
            response.Telefono = a.First(kvp => kvp.Key == "Telefono").Value.Content;
            response.Cargo = a.First(kvp => kvp.Key == "Cargo").Value.Content;
            response.Horario = a.First(kvp => kvp.Key == "Horario").Value.Content;
            response.Correo = a.First(kvp => kvp.Key == "Correo").Value.Content;
            response.NombreEmpresa = a.First(kvp => kvp.Key == "NombreEmpresa").Value.Content;
            response.Requisitos = a.First(kvp => kvp.Key == "Requisitos").Value.Content;
            response.Beneficios = a.First(kvp => kvp.Key == "Beneficios").Value.Content;
            response.Objetivo = a.First(kvp => kvp.Key == "Objetivo").Value.Content;
            response.Habilidades = a.First(kvp => kvp.Key == "Habilidades").Value.Content;
            response.Funciones = a.First(kvp => kvp.Key == "Funciones").Value.Content;

            return response;
        }
    }
}
