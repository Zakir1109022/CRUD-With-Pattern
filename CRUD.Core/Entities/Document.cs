using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.Core
{
    public abstract class Document : IDocument
    {
        public string Id { get; set; }
    }
}
