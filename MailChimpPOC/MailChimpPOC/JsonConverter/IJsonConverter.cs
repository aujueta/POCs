namespace MailChimpPOC.JSonConverter
{
    public interface IJsonConverter
    {
        TResult DeserializeObject<TResult>(string value);

        string SerializeObject(object obj);
    }
}
