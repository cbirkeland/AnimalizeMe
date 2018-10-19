using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalizeMe.Test.Mock
{
    class MockHostingEnvironment : IHostingEnvironment
    {
        public string EnvironmentName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string WebRootPath { get => @"C:\Project\AcceleratedLearning\AnimalizeMe.Test"; set => throw new NotImplementedException(); }
        public IFileProvider WebRootFileProvider { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string ContentRootPath { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IFileProvider ContentRootFileProvider { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
