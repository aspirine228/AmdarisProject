using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameTracker.Rep.Configurations
{
    public class SwaggerFileOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.OperationId == "Upload")
            {
                //operation.Parameters.Clear();
                /*operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "image",
                    In = ParameterLocation.Header,
                    Description = "Upload File",
                    Required = true,
                    Schema = new OpenApiSchema
                    {
                        Type = "file",
                        Format = "binary"
                    }
                });*/
                var uploadFileMediaType = new OpenApiMediaType()
                {
                    Schema = new OpenApiSchema()
                    {
                        Type = "object",
                        Properties =
                    {
                        ["image"] = new OpenApiSchema()
                        {
                            Description = "Upload File",
                            Type = "file",
                            Format = "binary"

                        }
                    },
                        Required = new HashSet<string>()
                    {
                        "image"
                    }
                    }
                };
                operation.RequestBody = new OpenApiRequestBody
                {
                    Content =
                {
                    ["multipart/form-data"] = uploadFileMediaType
                }
                };


            }
        }
    }
}
