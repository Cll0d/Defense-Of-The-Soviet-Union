using UnityEngine;
using UnityEngine.EventSystems;

public class ArmyManager : Loader<ArmyManager>
{
    private ArmyPanel armyBtnPressed;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Create2();
        }
    }

    public void SelectedArmy(ArmyPanel armySelcted)
    {
        armyBtnPressed = armySelcted;
        Debug.Log("Pressed" + armyBtnPressed.gameObject);
    }
    private void Create2()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && armyBtnPressed != null)
        {
            GameObject unitArmy = Instantiate(armyBtnPressed.ArmyObject);
            Debug.DrawRay(transform.position, transform.forward, Color.yellow);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Debug.Log("работант");
            if (Physics.Raycast(ray, out hit))
            {
                unitArmy.transform.position = hit.point;
            }
        }
    }
}

