global using System.Text.Json;
global using Microsoft.AspNetCore.Http;
global using Siva.Marriages.Business.DB;
global using Siva.Marriages.Business.Models;
global using Siva.Marriages.Business.DB.Models;
global using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siva.Marriages.Business
{
    public static class Constants
    {
        public const string tempData = "tempData";
        public const string jsonFileName = "data.json";
        public const string jsonFileID = "jsonFileID";
        public const string picturesFolder = "pictures";
    }
}
