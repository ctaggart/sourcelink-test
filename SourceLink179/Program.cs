using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata;

namespace SourceLink
{
    class Program
    {

        public static IEnumerable<Document> GetDocuments(string path)
        {
            using (var file = File.OpenRead(path))
            using (var mrp = MetadataReaderProvider.FromPortablePdbStream(file))
            {
                var mr = mrp.GetMetadataReader();
                foreach (var dh in mr.Documents)
                {
                    //Console.WriteLine("dh: " + dh.IsNil);
                    var d = mr.GetDocument(dh);
                    //var name = mr.GetString(d.Name);
                    //var language = mr.GetGuid(d.Language);
                    //var hashAlgorithm = mr.GetGuid(d.HashAlgorithm);
                    //var hash = mr.GetBlobBytes(d.Hash);
                    //var isEmbedded = IsEmbedded(mr, dh);
                    //Console.WriteLine("name: {0}", name);

                    yield return new Document
                    {
                        Name = mr.GetString(d.Name),
                        Language = mr.GetGuid(d.Language),
                        HashAlgorithm = mr.GetGuid(d.HashAlgorithm),
                        Hash = mr.GetBlobBytes(d.Hash),
                        IsEmbedded = IsEmbedded(mr, dh)
                    };
                }
            }
        }

        static void Main(string[] args)
        {


            foreach (var doc in GetDocuments(@"..\..\Paket.Core.pdb"))
            {
                Console.WriteLine("name: {0}", doc.Name);
            }
        }



        public static readonly Guid EmbeddedSourceId = new Guid("0E8A571B-6926-466E-B4AD-8AB04611F5FE");

        public static bool IsEmbedded(MetadataReader mr, DocumentHandle dh)
        {
            foreach (var cdih in mr.GetCustomDebugInformation(dh))
            {
                var cdi = mr.GetCustomDebugInformation(cdih);
                if (mr.GetGuid(cdi.Kind) == EmbeddedSourceId)
                    return true;
            }
            return false;
        }
    }
}
