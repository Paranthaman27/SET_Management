using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using SET_Management.Interface;
using SET_Management.Models.DTO;

namespace SET_Management.Helpers.Middlewares
{
    public class globalExceptionHandlingMiddleware : IMiddleware
    {
        private IApiResponseRepository _apiResponseRepository;

        public globalExceptionHandlingMiddleware(IApiResponseRepository apiResponseRepository)
        {
            _apiResponseRepository = apiResponseRepository;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                var controllerDescription = context.GetEndpoint().Metadata.GetMetadata<ControllerActionDescriptor>();
                var controllerName = controllerDescription.ControllerName;
                var actionName = controllerDescription.ActionName;
                StreamWriter sw;

                string logFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
                if (!Directory.Exists(logFolderPath))
                {
                    Directory.CreateDirectory(logFolderPath);
                }
                var logFileName = Path.Combine(logFolderPath, $"{DateTime.Now.ToString("ddMMyyyy")}_ErrorLog.txt");

                if (!Directory.Exists(logFileName))
                {
                    Directory.CreateDirectory(logFileName);
                    // sw = File.CreateText(logFileName);
                    //sw.Flush();
                    //sw.Close();
                }
                //Directory.GetFiles(logFileName);
                sw = new StreamWriter(logFileName);
                string Lineseprator = "=";

                sw.WriteLine(string.Concat(Enumerable.Repeat(Lineseprator, 50)));
                sw.WriteLine("Date and Time    : " + DateTime.Now.ToString("F"));
                sw.WriteLine("Controller Name  : " + controllerName);
                sw.WriteLine("Action Name      : " + actionName);
                sw.WriteLine("Error            : " + ex);

                sw.Flush();
                sw.Close();

                Directory.GetFiles(logFolderPath).Select(f => new FileInfo(f)).Where(f => f.CreationTime < DateTime.Now.AddDays(-7)).ToList().ForEach(f => f.Delete());
                var problemDetails = _apiResponseRepository.FailureResponse(new ApiResponseDTO { message = "Error has Occured", data = ex });
                await context.Response.WriteAsJsonAsync(problemDetails);

            }

        }


    }
}
