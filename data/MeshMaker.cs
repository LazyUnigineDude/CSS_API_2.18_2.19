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
	File image;

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

		//HisMeshMaker his;
		//his.GenerateMeshFromGrid();

    }

	void Shutdown()
	{
		mesh.Clear();
		mesh.DeleteLater();
		MeshDynamic.DeleteLater();
	}
}

class HisMeshMaker {

	Mesh mesh;
	ObjectMeshDynamic dynamicMesh;

public void GenerateMeshFromGrid(ivec2[][] grid)
    {
        List<vec3> vertices = new List<vec3>();
        List<int> indices = new List<int>();

        int width = grid.GetLength(0);
        int height = grid.GetLength(1);
        
        mesh = new Mesh();
        // Generate vertices based on traversable points
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
				ivec2 check; check.x = x; check.y = y;

                // Check if point is traversable
                if (grid[x][y] == check) vertices.Add(new vec3(x, y, 0));
            }
        }

        // Create triangles for the mesh
        for (int y = 0; y < height - 1; y++)
        {
            for (int x = 0; x < width - 1; x++)
            {
                // Indices for two triangles making a square
                int topLeft = y * width + x;
                int topRight = y * width + (x + 1);
                int bottomLeft = (y + 1) * width + x;
                int bottomRight = (y + 1) * width + (x + 1);

                // Triangle 1
                indices.Add(topLeft);
                indices.Add(bottomLeft);
                indices.Add(topRight);

                // Triangle 2
                indices.Add(topRight);
                indices.Add(bottomLeft);
                indices.Add(bottomRight);
            }
        }

        // Set vertices and indices to dynamic mesh
        //dynamicMesh.AllocateIndices(indices.Count);
        //dynamicMesh.AllocateVertex(vertices.Count);

        Log.Warning("So far no errors happened");

        int surface = mesh.AddSurface("surface");
       
        for (int i = 0; i < vertices.Count; i++)
        {
            mesh.AddVertex(vertices[i],surface);
        }

        for (int i = 0; i < indices.Count; i++)
        {
            mesh.AddIndex(indices[i],surface);
        }

        mesh.CreateTangents();
        mesh.CreateBounds();
        dynamicMesh = new ObjectMeshDynamic(mesh);
        dynamicMesh.WorldPosition = vec3.ZERO;
        dynamicMesh.Name = "Example";
    }

}