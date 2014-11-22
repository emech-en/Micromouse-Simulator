using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace MicroMouseSimul.MicroMouse
{
    class ObjectSaver
    {
        public static bool Save(string pFileName, Object pObject)
        {
            // Gain code access to the file that we are going
            // to write to
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();

                // Create a FileStream that will write data to file.
                FileStream writerFileStream =
                    new FileStream(pFileName, FileMode.Create, FileAccess.Write);
                // Save our dictionary of friends to file
                formatter.Serialize(writerFileStream, pObject);

                // Close the writerFileStream when we are done.
                writerFileStream.Close();

                return true;
            }
            catch
            {
                Console.WriteLine("Unable to save our friends' information");

                return false;
            } // end try-catch
        } // end public bool Load()

        public static object Load(string pFileName)
        {
            // Gain code access to the file that we are going
            // to write to
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();

                // Create a FileStream that will write data to file.
                FileStream readerFileStream =
                    new FileStream(pFileName, FileMode.Open, FileAccess.Read);
                // Save our dictionary of friends to file
                object retObject = formatter.Deserialize(readerFileStream);

                // Close the writerFileStream when we are done.
                readerFileStream.Close();

                return retObject;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to save our friends' information" + ex.Message);

                return null;
            } // end try-catch
        } // end public bool Load()
    }
}
