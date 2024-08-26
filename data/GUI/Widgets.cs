using System;
using System.Collections;
using System.Collections.Generic;
using Unigine;

[Component(PropertyGuid = "a044ac6f354bab8943cef6e51922ded8908f4fc9")]
public class Widgets : Component
{
    [ShowInEditor]
    List<AssetLink> ImageFile;

    [ShowInEditor]
    Material MaterialFile;

    [ShowInEditor, ParameterFile(Filter = ".ogv")]
    File VideoFile;

    [ShowInEditor, ParameterFile(Filter = ".mp3")]
    File SoundFile;

    [ShowInEditor]
    Node GUINode, SoundNode;

    WidgetLabel Label;
    WidgetButton Button;
    WidgetSlider Slider;
    WidgetCanvas Canvas;
    WidgetSprite Sprite;
    WidgetSpriteShader Shader;
    WidgetSpriteVideo Video;
    WidgetDialogColor Color;

    SoundSource Sound;
    AmbientSource Ambient;

    int hSize = 128;
    float Angle = 0;

    void Enter(Widget widget)
    {
        WidgetButton widgetButton = widget as WidgetButton;
        if (widgetButton != null) { widgetButton.ButtonColor = vec4.RED; }
    }

    void Leave(Widget widget)
    {
        WidgetButton widgetButton = widget as WidgetButton;
        if (widgetButton != null) { widgetButton.ButtonColor = vec4.WHITE; }
    }

    void Clicked(Widget widget) => Log.Message("Clicked\n");
    
    void Init()
	{
        // write here code to be called on component initialization

        // Label
        Label = new WidgetLabel("HELLO TO GUI");
        Label.FontSize = 21;
        Label.FontColor = vec4.RED;
        Label.FontOutline = 2;
        Label.SetPosition(50, 50);
        Label.EventEnter.Connect(() => { Log.Message("Hi\n"); });
        Label.EventLeave.Connect(() => { Log.Message("Bi\n"); });

        // Button
        Button = new WidgetButton("Click me");
        Button.Width = 50;
        Button.FontSize = 21;
        Button.SetPosition(400, 400);
        Button.FontColor = vec4.RED;
        Button.ButtonColor = vec4.BLACK;
        Button.Order = 1;

        Button.EventClicked.Connect(Clicked);
        Button.EventEnter.Connect(Enter);
        Button.EventLeave.Connect(Leave);

        // Slider
        Slider = new(0, 100, 50);
        Slider.Orientation = 0;
        Slider.Height = 250;
        Slider.ButtonHeight = 100;
        Slider.SetPosition(100, 150);
        Slider.ButtonColor = vec4.BLUE;
        Slider.EventChanged.Connect(() => { Log.Message($"{Slider.Value}\n"); });

        // Canvas
        Canvas = new();
        Canvas.SetPosition(400, 400);

        int sq = Canvas.AddPolygon(); // 200, 200
        Canvas.SetPolygonColor(sq, vec4.GREEN);
        Canvas.AddPolygonPoint(sq, vec3.ZERO);
        Canvas.AddPolygonPoint(sq, vec3.RIGHT * 200);
        Canvas.AddPolygonPoint(sq, (vec3.RIGHT + vec3.FORWARD) * 200);
        Canvas.AddPolygonPoint(sq, vec3.FORWARD * 200);

        int line = Canvas.AddLine(1);
        Canvas.SetLineColor(line, vec4.RED);
        Canvas.AddLinePoint(line, new vec2(10, 100));
        Canvas.AddLinePoint(line, new vec2(50, 150));

        int text = Canvas.AddText();
        Canvas.SetTextSize(text, 26);
        Canvas.SetTextText(text, "Hello");
        Canvas.SetTextColor(text, vec4.BLUE);
        Canvas.SetTextPosition(text, vec2.ONE * 100); //Pos 100, 100

        // Add Image to Canvas Shape
        int Image = Canvas.AddPolygon(1); // 200x200 at pos 400, 0
        Canvas.SetPolygonTexture(Image, ImageFile[0].AbsolutePath); // HardCoding Positions

        Canvas.AddPolygonPoint(Image, vec3.RIGHT * 200); // 200, 0
        Canvas.SetPolygonTexCoord(Image, vec2.ZERO); // 0, 0

        Canvas.AddPolygonPoint(Image, vec3.RIGHT * 400); // 400, 0
        Canvas.SetPolygonTexCoord(Image, new vec2(1, 0)); //  1, 0

        Canvas.AddPolygonPoint(Image, vec3.RIGHT * 400 + vec3.FORWARD * 200);
        Canvas.SetPolygonTexCoord(Image, vec2.ONE); // 400, 200 & 1, 1

        Canvas.AddPolygonPoint(Image, (vec3.RIGHT + vec3.FORWARD) * 200);
        Canvas.SetPolygonTexCoord(Image, new vec2(0, 1)); // 200, 200 & 0, 1

        // Image
        Sprite = new WidgetSprite();
        foreach (var item in ImageFile)
        {
            int x = Sprite.AddLayer();
            Sprite.SetLayerTexture(x, item.AbsolutePath);

            mat4 mat = Sprite.GetLayerTransform(x);
            mat.Translate = (vec3.LEFT + vec3.BACK) * hSize;
            Sprite.SetLayerTransform(x, mat);
        }
        Sprite.SetPosition(500, 150);

        // Image with Shaders
        Shader = new WidgetSpriteShader();
        Shader.SetPosition(800 - hSize, 150 - hSize);
        Shader.Texture = "white.texture";
        Shader.Width = hSize * 2;
        Shader.Height = hSize * 2;
        Shader.Material = MaterialFile;

        // Video
        ObjectGui VGui = GUINode as ObjectGui;
        Video = new WidgetSpriteVideo(VideoFile.GetName());
        Video.Width = VGui.ScreenWidth;
        Video.Height = VGui.ScreenHeight;
        Video.Loop = 1;
        Video.Play();

        Sound = SoundNode as SoundSource;
        Sound.Play();
        Video.SoundSource = Sound;

        //// Works
        //Ambient = new AmbientSource(SoundFile.GetName());
        //Ambient.Play();
        //Ambient.Loop = 1;
        //Video.AmbientSource = Ambient;

        // Dialog Color
        Color = new WidgetDialogColor();
        Color.CloseText = "Close the box my guyy";
        Color.CancelText = "Cancel the changes my dudes";
        Color.OkText = "OK Dudes we pick this";
        Color.Sizeable = true;
        Color.SetPosition(800, 400);
        Color.Width = 200;
        Color.Height = 200;
        Color.EventClicked.Connect(() => {
            if (Color.IsCancelClicked() == 1) Log.Message("Clicked Cancel\n");
            else if (Color.IsOkClicked) Log.Message("Clicked Okay\n");
            else if (Color.IsCloseClicked() == 1) Log.Message("Clicked Close\n");
        });

        Unigine.Gui GUI = Gui.GetCurrent();
        GUI.AddChild(Label, Gui.ALIGN_EXPAND | Gui.ALIGN_OVERLAP);
        GUI.AddChild(Button, Gui.ALIGN_EXPAND | Gui.ALIGN_OVERLAP);
        GUI.AddChild(Slider, Gui.ALIGN_EXPAND | Gui.ALIGN_OVERLAP);
        GUI.AddChild(Canvas, Gui.ALIGN_EXPAND | Gui.ALIGN_OVERLAP);
        GUI.AddChild(Sprite, Gui.ALIGN_EXPAND | Gui.ALIGN_OVERLAP);
        GUI.AddChild(Shader, Gui.ALIGN_EXPAND | Gui.ALIGN_OVERLAP);
        GUI.AddChild(Color, Gui.ALIGN_OVERLAP);

        // Add Video to Object in World
        // THE GUI Inside the GUIObject
        Gui VGUIP = VGui.GetGui();
        VGUIP.AddChild(Video, Gui.ALIGN_EXPAND | Gui.ALIGN_OVERLAP);
    }

    void Update()
	{
        // write here code to be called before updating each render frame
        if (Input.IsKeyPressed(Input.KEY.U)) RotateImage(false);
        if (Input.IsKeyPressed(Input.KEY.I)) RotateImage(true);

        //if (Input.IsKeyDown(Input.KEY.J)) Ambient.Stop();
        //if (Input.IsKeyDown(Input.KEY.K)) Ambient.Play();
        //if (Input.IsKeyDown(Input.KEY.L)) { Ambient.Stop(); Ambient.Time = 0; Ambient.Play(); }

        if (Input.IsKeyDown(Input.KEY.J)) Sound.Stop();
        if (Input.IsKeyDown(Input.KEY.K)) Sound.Play();
        if (Input.IsKeyDown(Input.KEY.L)) { Sound.Stop(); Sound.Time = 0; Sound.Play(); }
    }

    void Shutdown()
    {
        Unigine.Gui GUI = Gui.GetCurrent();
        if (GUI.IsChild(Label) == 1) GUI.RemoveChild(Label);
        if (GUI.IsChild(Button) == 1) GUI.RemoveChild(Button);
        if (GUI.IsChild(Slider) == 1) GUI.RemoveChild(Slider);
        if (GUI.IsChild(Canvas) == 1) GUI.RemoveChild(Canvas);
        if (GUI.IsChild(Sprite) == 1) GUI.RemoveChild(Sprite);
        if (GUI.IsChild(Shader) == 1) GUI.RemoveChild(Shader);
        if (GUI.IsChild(Color) == 1) GUI.RemoveChild(Color);

        Label.DeleteLater();
        Button.DeleteLater();
        Slider.DeleteLater();
        Canvas.DeleteLater();
        Sprite.DeleteLater();
        Shader.DeleteLater();
        Color.DeleteLater();

        // Video
        ObjectGui VGui = GUINode as ObjectGui;
        Gui VGUIP = VGui.GetGui();
        if (VGUIP.IsChild(Video) == 1) VGUIP.RemoveChild(Video);
        Sound.Stop();
        //Ambient.Stop();
        Video.DeleteLater();
    }

    void RotateImage(bool ClockWise)
    {

        // cos0 - sin0
        // cos0 + sin0
        // 0

        if (!ClockWise) Angle += Game.IFps * 36;
        else            Angle -= Game.IFps * 36;
        Angle = MathLib.Clamp(Angle, 0.0f, 250.0f);

        int RPM = (int)(10000 * (Angle / 250));
        Log.Message($"RPM: {RPM}\n");

        vec3 PIV = vec3.ZERO;
        PIV.x = (MathLib.Cos(Angle * MathLib.DEG2RAD) - MathLib.Sin(Angle * MathLib.DEG2RAD)) * -hSize;
        PIV.y = (MathLib.Cos(Angle * MathLib.DEG2RAD) + MathLib.Sin(Angle * MathLib.DEG2RAD)) * -hSize;

        quat Rot = new quat( 0.0f, 0.0f, Angle );
        mat4 Mat = new mat4(Rot, PIV);
        Sprite.SetLayerTransform(ImageFile.Count, Mat);
    }
}