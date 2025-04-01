using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClickScript : MonoBehaviour {
    public Camera mainCamera;
    public Texture2D splashTexture;

    private const int c_colorArrayLength = 6;
    private readonly Color[] c_colorArray = new Color[c_colorArrayLength] { Color.blue, Color.yellow, Color.red, Color.green, Color.white, Color.black };
    private int m_currColor = 0;
	
	void Update ()
    {
        
        if (Mouse.current.leftButton.isPressed)
        {
            /*RaycastHit hit;
            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit))
            {
                MyShaderBehavior script = hit.collider.gameObject.GetComponent<MyShaderBehavior>();
                if (null != script)
                    script.PaintOnColored(hit.textureCoord2, PaintProjectileManagerStatic.GetInstance().GetRandomProjectileSplash(), PaintProjectileManagerStatic.GetInstance().paintBombColor);
            }*/
            GameObject projectile = Instantiate(PaintProjectileManager.GetInstance().paintBombPrefab, mainCamera.transform.position + mainCamera.transform.forward * 3, mainCamera.transform.rotation);
            projectile.GetComponent<PaintProjectileBehavior>().paintColor = PaintProjectileManager.GetInstance().paintBombColor;
            projectile.GetComponent<Rigidbody>().linearVelocity = mainCamera.transform.forward * 25;
        }

        if (Mouse.current.rightButton.isPressed)
        {
            PaintProjectileManager.GetInstance().paintBombColor = c_colorArray[m_currColor];
            m_currColor++;
            if (m_currColor == c_colorArrayLength)
                m_currColor = 0;
        }
	}
}
