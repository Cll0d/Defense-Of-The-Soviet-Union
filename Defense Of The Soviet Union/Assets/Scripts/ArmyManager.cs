using UnityEngine;
using UnityEngine.EventSystems;

public class ArmyManager : Loader<ArmyManager>
{
    private ArmyPanel armyBtnPressed;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Create();
            //CreateObjToKursorPos();
            //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector3.zero);
            //CreatUnitArmy(hit);
        }
    }

    public void SelectedArmy(ArmyPanel armySelcted)
    {
        armyBtnPressed = armySelcted;
        Debug.Log("Pressed" + armyBtnPressed.gameObject);
    }

    private void CreatUnitArmy(RaycastHit2D hit)
    {
        if (!EventSystem.current.IsPointerOverGameObject() && armyBtnPressed != null)
        {
            GameObject unitArmy = Instantiate(armyBtnPressed.ArmyObject);
            unitArmy.transform.position = hit.transform.position;
        }
    }
    private void CreateObjToKursorPos()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && armyBtnPressed != null)
        {
            GameObject unitArmy = Instantiate(armyBtnPressed.ArmyObject);
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var pos = ray.origin + ray.direction * 10.0f;
            unitArmy.transform.position = pos;
        }
    }
    private void Create()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && armyBtnPressed != null)
        {
            GameObject unitArmy = Instantiate(armyBtnPressed.ArmyObject);
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 60;
            Vector3 mouseGlob = Camera.main.ScreenToWorldPoint(mousePos);
            unitArmy.transform.position = mouseGlob;
        }
    }
    public Transform objectToCreate; // тот объект, который хотите создавать

    void cer()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouse = Input.mousePosition;//узнаём координаты мыши
            mouse.z = 10; //  на каком расстоянии от камеры по z будет находиться объект,
                          //   это для того, чтобы объект не появлялся прямо там, где позиция камеры

            Vector3 mouseGlob = Camera.main.ScreenToWorldPoint(mouse); // находим мышь в 3d пространстве

            Instantiate(objectToCreate, mouseGlob, Quaternion.identity); // создаём объект в нужном позиции
        }
    }
}

