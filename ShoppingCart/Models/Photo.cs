﻿using System;
using System.Collections.Generic;

#nullable disable

namespace ShoppingCart.Models
{
    public partial class Photo
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string PublicId { get; set; }
        public int Version { get; set; }
        public int? BookId { get; set; }
        public string Signature { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Format { get; set; }
        public string ResourceType { get; set; }
        public int Bytes { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
        public string SecureUrl { get; set; }
        public string Path { get; set; }
    }
}
