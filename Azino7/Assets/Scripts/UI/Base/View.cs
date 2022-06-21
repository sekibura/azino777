using UnityEngine;

public abstract class View : MonoBehaviour
{
    // ����������� �����, �� ������ �������� �������� ��� ������ ���� ����������������� ����������
    public abstract void Initialize();
    public virtual void Hide() => gameObject.SetActive(false);
    public virtual void Show(object parameter = null) => gameObject.SetActive(true);
}
