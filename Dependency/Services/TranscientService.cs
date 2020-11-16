using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dependency.Services
{
    public class TranscientService : ITranscientService
    {
        private Guid _guid;

        public SingletonService()
        {
            _guid = Guid.NewGuid();
        }
        public Guid ToonGuid()
        {
            return _guid;
        }
    }
}
