using System;
using System.Collections.Generic;

namespace MusicStore.Web.Shared
{
    public class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public IEnumerable<Album> Albums { get; set; } = new List<Album>();
    }
}
