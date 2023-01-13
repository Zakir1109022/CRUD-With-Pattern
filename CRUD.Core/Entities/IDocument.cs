using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.Core
{
    public interface IDocument
    {
        [BsonId]
        string Id { get; set; }

    }
}
