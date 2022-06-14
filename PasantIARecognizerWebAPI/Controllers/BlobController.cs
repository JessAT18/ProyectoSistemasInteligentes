using Microsoft.AspNetCore.Mvc;

namespace PasantIARecognizerWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlobController : Controller
    {
        private static readonly string blobContainerConnStr = "DefaultEndpointsProtocol=https;AccountName=formanalyzerstorage;AccountKey=CluKUgu+1sYSV5zbIi13lTm8am7tjgHjK4hEkZFSaLTPJrdy5RQciu1hZij789AiZ4m0JKK/tzDf45QCxDIn5w==;EndpointSuffix=core.windows.net";
        private static readonly string containerName = "democontainer";

        //[HttpGet(Name = "SubirImagenABlob")]

    }
}
