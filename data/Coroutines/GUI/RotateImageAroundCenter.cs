using System.Collections;
using System.Collections.Generic;
using Unigine;
using static System.Net.Mime.MediaTypeNames;
using static Unigine.Plugins.Kinect;

[Component(PropertyGuid = "17d209661327601a3741446320943169afc8dfa2")]
public class RotateImageAroundCenter : Component
{
    public AssetLink MainImage;
    WidgetSprite _Sprite;
    int layer, hWidth, hHeight;
    public int width = 200, height = 200;
    float rotateAmount = 0;
    
	void Init()
	{
        // write here code to be called on component initialization
        Gui GUI = Gui.GetCurrent();

        hWidth = (int)(width * 0.5);
        hHeight = (int)(height * 0.5);

        // Create Object
        _Sprite = new();
        Unigine.Image image = new(MainImage.AbsolutePath); 
        image.Resize(width, height);

        // Not Touching the base, the New Layer is what we will add
        layer = _Sprite.AddLayer();
        _Sprite.SetLayerImage(layer, image);

        // Center the Layer on the middle
        mat4 mL = _Sprite.GetLayerTransform(layer);
        mL.Translate = new vec3(-hWidth,-hHeight, 0);
        _Sprite.SetLayerTransform(layer, mL);

        // Add to GUI in center for now
        GUI.AddChild( _Sprite , Gui.ALIGN_CENTER);

    }
    void Update()
    {
        // write here code to be called before updating each render frame

        if (Input.IsKeyPressed(Input.KEY.U)) { rotateAmount -= Game.IFps * 36; rotateAmount %= 360; RotateImage(); }
        if (Input.IsKeyPressed(Input.KEY.I)) { rotateAmount += Game.IFps * 36; rotateAmount %= 360; RotateImage(); }
    }

	void Shutdown()
	{
        Gui GUI = Gui.GetCurrent();
        if (GUI.IsChild(_Sprite) == 1) { GUI.RemoveChild(_Sprite); _Sprite.DeleteLater(); }
    }

    void RotateImage()
    {
        vec3 piv = new vec3(  // obj translation with Vectors
                     (MathLib.Cos(rotateAmount * MathLib.DEG2RAD) * -hWidth) - (MathLib.Sin(rotateAmount * MathLib.DEG2RAD) * -hHeight),
                     (MathLib.Cos(rotateAmount * MathLib.DEG2RAD) * -hHeight) + (MathLib.Sin(rotateAmount * MathLib.DEG2RAD) * -hWidth),
                     0
                 );

        quat rot = new quat(0, 0, rotateAmount); // rotation
        mat4 obj = new mat4(rot, piv);

        _Sprite.SetLayerTransform(layer, obj);
    }
}