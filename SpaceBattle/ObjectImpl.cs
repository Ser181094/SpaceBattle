using SpaceBattle.Interfaces;

namespace SpaceBattle
{
    public class ObjectImpl : IUObject
    {
        Dictionary<string, object> values = new Dictionary<string, object>();

        public object GetProperty(string propName)
        {
            values.TryGetValue(propName, out var value);
            return value;
        }

        public void SetProperty(string propName, object newVal)
        {
            if (values.ContainsKey(propName))
            {
                values[propName] = newVal;
            } else
            {
                values.Add(propName, newVal);
            }
        }
    }
}
