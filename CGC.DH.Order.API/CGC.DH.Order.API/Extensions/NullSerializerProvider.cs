using System;
using System.Web.OData.Formatter.Serialization;
using Microsoft.Owin;
using Microsoft.OData.Edm;
using Microsoft.OData.Core;
using System.Net.Http;
using System.Net;


namespace CGC.DH.Order.API.Extensions
{    
    public class NullSerializerProvider : DefaultODataSerializerProvider
    {
        private readonly NullEntityTypeSerializer _nullEntityTypeSerializer;

        public NullSerializerProvider()
        {
            _nullEntityTypeSerializer = new NullEntityTypeSerializer(this);
        }

        public override ODataSerializer GetODataPayloadSerializer(IEdmModel model, Type type, HttpRequestMessage request)
        {
            var serializer = base.GetODataPayloadSerializer(model, type, request);
            if (serializer == null)
            {
                var response = request.GetOwinContext().Response;
                response.OnSendingHeaders(state =>
                {
                    ((IOwinResponse)state).StatusCode = (int)HttpStatusCode.NotFound;
                }, response);
                return _nullEntityTypeSerializer;
            }
            return serializer;
        }
    }
    
    public class NullEntityTypeSerializer : ODataEntityTypeSerializer
    {
        public NullEntityTypeSerializer(ODataSerializerProvider serializerProvider)
            : base(serializerProvider)
        { }

        public override void WriteObjectInline(object graph, IEdmTypeReference expectedType, ODataWriter writer, ODataSerializerContext writeContext)
        {
            if (graph != null)
            {
                base.WriteObjectInline(graph, expectedType, writer, writeContext);
            }
        }
    }
}