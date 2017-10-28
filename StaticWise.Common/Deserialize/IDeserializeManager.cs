using StaticWise.Entities;

namespace StaticWise.Common.Deserialize
{
    public interface IDeserializeManager
    {
        /// <summary>
        /// Deserialize a config file (stored as JSON) into a Config object
        /// </summary>
        /// <param name="filePath">The path and filename of the file (e.g. "C:\config.json")</param>
        /// <returns>A Config object that will contain all data stored in the file</returns>
        Config DeserializeConfig(string path);

        /// <summary>
        /// Deserialize a post file (including front matter) into a Post object
        /// </summary>
        /// <param name="filePath">The path and filename of the file (e.g. "C:\post.md")</param>
        /// <param name="dateFormat">The date format to use when deserializing</param>
        /// <returns>A Post object that will contain all data stored in the file</returns>
        Post DeserializePost(string path, string dateFormat);

        /// <summary>
        /// Deserialize a page file (including front matter) into a Page object
        /// </summary>
        /// <param name="filePath">The path and filename of the file (e.g. "C:\page.md")</param>
        /// <returns>A Page object that will contain all data stored in the file</returns>
        Page DeserializePage(string path);
    }
}