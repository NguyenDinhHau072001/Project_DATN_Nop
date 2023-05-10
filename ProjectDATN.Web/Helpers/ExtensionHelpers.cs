using System.Text.Json;

namespace ProjectDATN.Web.Helpers
{
    public static class ExtensionHelpers
    {
        public static string FormatText(string value)
        {
            if (value == null)
                return "";
            if(value.Length <40)
                return value;
            return value[..40] + "...";
        }

        public static string TinhTrang(int value)
        {
            if (value == 0) return "Chờ xử lý";
            if (value == 1) return "Đã xác nhận";
            if (value == 2) return "Đang vận chuyển";
            if (value == 3) return "Đã nhận hàng";
            if (value == 4) return "Đã hủy";

            return "";
        }
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T? Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }

    }
}
