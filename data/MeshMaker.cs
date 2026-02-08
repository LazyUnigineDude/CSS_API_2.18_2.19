using System.Collections;
using System.Collections.Generic;
using Unigine;

[Component(PropertyGuid = "8b3e1e13401f489b79cd8ce34952726be1ea7586")]
public class MeshMaker : Component
{
	List<vec3> vertices = new();
	Mesh mesh;
	ObjectMeshDynamic MeshDynamic;

	[ShowInEditor]
	AssetLink image;

    HisMeshMaker meshMaker;


	void Init()
	{
        // write here code to be called on component initialization
        // vertices.Add(vec3.FORWARD);
        // vertices.Add(vec3.BACK + vec3.LEFT);
        // vertices.Add(vec3.BACK + vec3.RIGHT);

        // mesh = new Mesh();
        // int _surface = mesh.AddSurface();

        // for (int i = 0; i < vertices.Count; i++)
        // {
        // 	mesh.AddVertex(vertices[i], _surface);
        // 	mesh.AddIndex(i, _surface);
        // }
        // mesh.CreateTangents();
        // mesh.CreateBounds();

        // MeshDynamic = new ObjectMeshDynamic(mesh);
        // MeshDynamic.WorldPosition = vec3.ZERO;
        // MeshDynamic.Name = "Example";

        Image img = new Image(image.AbsolutePath);
         meshMaker = new HisMeshMaker(img);
    }

	void Shutdown()
	{
        meshMaker.Dispose();
	}
}

class HisMeshMaker {

	Mesh mesh;
	ObjectMeshDynamic dynamicMesh;
    int width = 2, height = 2, raise = 2;

    public HisMeshMaker()
    {
        mesh = new Mesh();
        dynamicMesh = new ObjectMeshDynamic();
    }

    public HisMeshMaker(Image Image)
    {
        List<ivec3> vertices = new List<ivec3>();
        List<int> indices = new List<int>();

        //width = Image.Width;
        //height = Image.Height;

        mesh = new Mesh();
        mesh.AddSurface();

        // Generate vertices based on traversable points
        for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
            {
                ivec2 check; check.x = x; check.y = y;
                mesh.AddVertex(new ivec3(x, y, 0/*Image.Get8F(check) * raise*/), 0);
            }

        for (int y = 0; y < height - 1; y++)
            for (int x = 0; x < width - 1; x++)
            {
                int TL = y * width + x,
                    TR = y * width + (x + 1),
                    BL = (y + 1) * width + x,
                    BR = (y + 1) * width + (x + 1);

                mesh.AddIndex(BL, 0);
                mesh.AddIndex(TR, 0);
                mesh.AddIndex(TL, 0);
            }


        mesh.CreateTangents();
        mesh.CreateBounds();
        dynamicMesh = new ObjectMeshDynamic(mesh);
        dynamicMesh.WorldPosition = vec3.ZERO;
        dynamicMesh.Name = "Example";
    }

    public void Dispose()
    {
        mesh.Clear();
        mesh.Dispose();
        dynamicMesh.DeleteLater();
    }
}