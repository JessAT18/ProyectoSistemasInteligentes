using Azure;
using Azure.AI.FormRecognizer;
using Azure.AI.FormRecognizer.DocumentAnalysis;
using Azure.AI.FormRecognizer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace PasantIARecognizer
{
    class Program
    {
        private static readonly string endpoint = "https://jessformrecognizer.cognitiveservices.azure.com/";
        private static readonly string apiKey = "0bd31f6a8ad74efa95376576886179e4";
        private static readonly AzureKeyCredential credential = new AzureKeyCredential(apiKey);

        static async Task Main(string[] args)
        {
            string modelId = "PasantIA";
            Console.WriteLine("¡Bienvenido! A continuación se analizara la imagen contenida en el blob storage");
            //string filePath = Console.ReadLine();
            Uri fileUri = new Uri("https://formanalyzerstorage.blob.core.windows.net/trainimages/pasantia5.PNG");
            IDictionary<string, string> pasantia = new Dictionary<string, string>();
            //var analyzeFormTask = AnalyzeForm(modelId, filePath, pasantia);

            var client = new DocumentAnalysisClient(new Uri(endpoint), credential);

            //var stream = new FileStream(filePath, FileMode.Open);
            //AnalyzeDocumentOperation operation = await client.StartAnalyzeDocumentAsync(modelId, stream);
            AnalyzeDocumentOperation operation = await client.StartAnalyzeDocumentFromUriAsync(modelId, fileUri);
            Response<AnalyzeResult> operationResponse = await operation.WaitForCompletionAsync();
            AnalyzeResult result = operationResponse.Value;

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
            Console.WriteLine("¿Deseas guardar los datos? Si/No");
            string guardar = Console.ReadLine();


            //Task.WaitAll(analyzeFormTask);
            Console.WriteLine("Proceso terminado, presiona Enter para salir");
            Console.ReadLine();



        }

        //static void Main(string[] args)
        //{
        //    string modelId = "PasantIA";
        //    Console.WriteLine("¡Bienvenido! A continuación escribe la ruta completa donde se encuentra tu imagen");
        //    Console.WriteLine("Ejemplo: D:/Ejemplos/ejemplo2.jpg");
        //    string filePath = Console.ReadLine();
        //    IDictionary<string, string> pasantia = new Dictionary<string, string>();
        //    //var analyzeFormTask = AnalyzeForm(modelId, filePath, pasantia);

        //    FormRecognizerClient client = new FormRecognizerClient(new Uri(endpoint), credential);

        //    var stream = new FileStream(filePath, FileMode.Open);

        //    var options = new RecognizeCustomFormsOptions() { IncludeFieldElements = true };

        //    RecognizeCustomFormsOperation operation = await client.StartRecognizeCustomFormsAsync(modelId, stream, options);
        //    Response<RecognizedFormCollection> operationResponse = await operation.WaitForCompletionAsync();
        //    RecognizedFormCollection forms = operationResponse.Value;

        //    foreach (RecognizedForm form in forms)
        //    {
        //        Console.WriteLine($"Form of type: {form.FormType}");
        //        Console.WriteLine($"Form was analyzed with model with ID: {modelId}");
        //        foreach (FormField field in form.Fields.Values)
        //        {
        //            pasantia.Add(field.Name, field.ValueData.Text);
        //        }
        //    }

        //    foreach (KeyValuePair<string, string> dato in pasantia)
        //    {
        //        Console.WriteLine("{0}: {1}",
        //        dato.Key, dato.Value);
        //    }
        //    Console.WriteLine("¿Deseas guardar los datos? Si/No");
        //    string guardar = Console.ReadLine();


        //    //Task.WaitAll(analyzeFormTask);
        //    Console.WriteLine("Proceso terminado, presiona Enter para salir");
        //    Console.ReadLine();



        //}

        private static async Task AnalyzeForm(String modelId, string filePath, IDictionary<string, string> pasantia)
        {
            FormRecognizerClient client = new FormRecognizerClient(new Uri(endpoint), credential);

            var stream = new FileStream(filePath, FileMode.Open);

            var options = new RecognizeCustomFormsOptions() { IncludeFieldElements = true };

            RecognizeCustomFormsOperation operation = await client.StartRecognizeCustomFormsAsync(modelId, stream, options);
            Response<RecognizedFormCollection> operationResponse = await operation.WaitForCompletionAsync();
            RecognizedFormCollection forms = operationResponse.Value;

            foreach (RecognizedForm form in forms)
            {
                Console.WriteLine($"Form of type: {form.FormType}");
                Console.WriteLine($"Form was analyzed with model with ID: {modelId}");
                foreach (FormField field in form.Fields.Values)
                {
                    pasantia.Add(field.Name, field.ValueData.Text);
                }
            }

            foreach (KeyValuePair<string, string> dato in pasantia)
            {
                Console.WriteLine("{0}: {1}",
                dato.Key, dato.Value);
            }
            Console.WriteLine("¿Deseas guardar los datos? Si/No");
            string guardar = Console.ReadLine();
            //if (guardar == "Si")
            //{
            //    Factura f = new Factura();
            //    if (factura.ContainsKey("Nombre Estacion de Servicio"))
            //    {
            //        f.NombreEESS = factura["Nombre Estacion de Servicio"];
            //    }
            //    if (factura.ContainsKey("NIT/CI"))
            //    {
            //        f.Ci = factura["NIT/CI"];
            //    }
            //    if (factura.ContainsKey("Nombre"))
            //    {
            //        f.Nombre = factura["Nombre"];
            //    }
            //    if (factura.ContainsKey("Total Bs"))
            //    {
            //        f.TotalBs = System.Convert.ToDouble(factura["Total Bs"].Replace(",", "."));
            //    }
            //    if (factura.ContainsKey("Fecha"))
            //    {
            //        f.Fecha = factura["Fecha"];
            //    }
            //    if (factura.ContainsKey("Hora"))
            //    {
            //        f.Hora = factura["Hora"];
            //    }
            //    if (factura.ContainsKey("Placa Vehiculo"))
            //    {
            //        f.Placa = factura["Placa Vehiculo"];
            //    }
            //    if (factura.ContainsKey("Numero de Factura"))
            //    {
            //        f.IDFactura = System.Convert.ToInt32(factura["Numero de Factura"]);
            //    }
            //    else
            //    {
            //        Console.WriteLine("Escribe el ID de la factura");
            //        f.IDFactura = Convert.ToInt32(Console.ReadLine());
            //    }

            //    var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://apiproductorai.azurewebsites.net/api/factura");
            //    httpWebRequest.ContentType = "application/json";
            //    httpWebRequest.Method = "POST";

            //    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            //    {
            //        string json = JsonConvert.SerializeObject(f);
            //        streamWriter.Write(json);
            //    }

            //    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            //    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            //    {
            //        var result = streamReader.ReadToEnd();
            //    }
            //}
        }
    }
}
