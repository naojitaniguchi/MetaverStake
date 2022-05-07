using System;
using System.Linq;
using System.Text;
using UnityEngine;
//using UnityEngine.UI;
using TMPro;
public class QueryParseExample : MonoBehaviour
{
    // 現在のURLを表示するテキスト
    [SerializeField] private TextMeshProUGUI _urlText;

    private void Start()
    {
        // 現在のURLからUriインスタンスを生成
        // Unityエディタ環境では空文字でエラーとなることに注意！
        var uri = new Uri(Application.absoluteURL);

        // 「?」より後ろのクエリ文字列取得
        // 得られる文字列はエスケープ解除された状態
        var queryStr = uri.GetComponents(UriComponents.Query, UriFormat.SafeUnescaped);

        // クエリを解析し、Dictionary（Key-Value形式）に変換
        var queries = queryStr
            .Split('&') // Key-Valueのペアは「&」で区切られているので分割
            .Select(x => x.Split('=')) // 「Key=Value」の表現なので、「=」でKey-Valueを分割
            .Where(x => x.Length == 2) // Key-Valueの2つ以外は不正とみなす
            .ToDictionary(x => x[0], x => x[1]); // Key-ValueのペアをDictionaryに変換

        // Key-Valueの内容を表示
        var outputText = new StringBuilder();
        foreach (var query in queries)
        {
            outputText.AppendLine($"[{query.Key}] => {query.Value}");
        }

        _urlText.text = outputText.ToString();
    }
}
