using UnityEngine;

public abstract class View : MonoBehaviour
{
    // абстрактный класс, на основе которого строятся все классы окон пользовательского интерфейса
    public abstract void Initialize();
    public virtual void Hide() => gameObject.SetActive(false);
    public virtual void Show(object parameter = null) => gameObject.SetActive(true);
}
