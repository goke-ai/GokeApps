#nullable enable
using System.Collections.Generic;

namespace Microsoft.AspNetCore.Identity
{
    public class IdentityResult
    {
        public bool Succeeded { get; set; }
        public IEnumerable<IdentityError>? Errors { get; set; }
    }
}