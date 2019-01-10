using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    //una clase estatica tiene metodos y variables estaticos
    //esta clase no puede instanciarse ni ocuparse como componente para algun objeto de unity

    public static Vector2 GetDimensionsInWorldUnit()
    {
        float width, height;

        //guardamos la referencia de la camara principal en la variable cam de tipo CAMARA
        Camera cam = Camera.main;
        //dividimos los pixeles del ancho por los pixeles de alto de la pantalla.
        //le hacemos un cast a cualquier valor para que el resultado sea mas exacto al tener decimales.
        float ratio = cam.pixelWidth / (float)cam.pixelHeight;
        //calculamos el alto de la pantalla. Esto para una camara ortografica.
        height = cam.orthographicSize * 2;
        //calculamos el ancho de la pantalla
        width = height * ratio;

        return new Vector2(width, height);
    }
}
