using Azure;
using Azure.AI.FormRecognizer.DocumentAnalysis;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using PasantIARecognizerWebAPI.Models;

namespace PasantIARecognizerWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PasantiaController : Controller
    {
        //FORM RECOGNIZER
        private static readonly string endpoint = "https://jessformrecognizer.cognitiveservices.azure.com/";
        private static readonly string apiKey = "0bd31f6a8ad74efa95376576886179e4";
        private static readonly string modelId = "PasantIA";

        private static readonly AzureKeyCredential credential = new AzureKeyCredential(apiKey);

        //COSMOSDB
        private const string endpointCosmos = "https://base1jess.documents.azure.com:443/";
        private const string primaryKey = "FOoq58qUIklvMKv9CebQgnD7QRan6QLTFih0eG24W1486V1odFTtidzSZSm8BQFJOsr8cMjmZWlj4zhqm8gZ5w==";
        private const string databaseName = "pasantias";
        private const string containerName = "datos";


        [HttpGet(Name = "EjecutarPasantIARecognizer")]
        public async Task<PasantiaResponse> EjecutarPasantIARecognizerAsync(string uriBlobStorage)
        {
            Uri fileUri = new Uri(uriBlobStorage);
            IDictionary<string, string> pasantia = new Dictionary<string, string>();

            var client = new DocumentAnalysisClient(new Uri(endpoint), credential);

            AnalyzeDocumentOperation operation = await client.StartAnalyzeDocumentFromUriAsync(modelId, fileUri);
            Azure.Response<AnalyzeResult> operationResponse = await operation.WaitForCompletionAsync();
            AnalyzeResult result = operationResponse.Value;

            PasantiaResponse response = new PasantiaResponse();

            var a = result.Documents[0].Fields;

            response.id = DateTime.Today.ToString("yyyyMMddhhmm");
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
        [HttpPost(Name = "SubirACosmosDB")]
        public async Task SubirACosmosDB([FromBody] PasantiaResponse pasantia)
        {
            CosmosClient cosmosClient = new CosmosClient(endpointCosmos, primaryKey);

            Database database = await cosmosClient.CreateDatabaseIfNotExistsAsync(databaseName);
            Container container = await database.CreateContainerIfNotExistsAsync(containerName, "/id");

            pasantia.id = DateTime.Today.ToString("yyyyMMddhhmm");
            await container.CreateItemAsync(pasantia, new PartitionKey(pasantia.id));
        }
    }
}
