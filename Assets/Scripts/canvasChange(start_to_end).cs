using UnityEngine;

public class CanvasController : MonoBehaviour
{
    // シーン内のCanvasオブジェクト
    public Canvas startCanvas;

    void Start()
    {
        // シーン内のCanvasを表示
        ShowCanvas(startCanvas);
    }

    // Canvasを表示するメソッド
    void ShowCanvas(Canvas canvas)
    {
        if (canvas != null)
        {
            canvas.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("Canvas is null. Make sure the canvas exists in the scene.");
        }
    }
}
