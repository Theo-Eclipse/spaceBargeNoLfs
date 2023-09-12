
namespace UnityEngine.UI
{
    [RequireComponent(typeof(Button))]
    public class ButtonClickSfx : MonoBehaviour
    {
        [SerializeField] private string soundName;
        private Button button;
        // Start is called before the first frame update
        void Start()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(Play);
        }
        void Play() => AudioManager.PlaySfx(soundName, Vector3.zero);
    }
}
