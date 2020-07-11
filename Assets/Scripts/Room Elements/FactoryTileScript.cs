using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FactoryTileScript : Tile
{
    public Sprite[] m_Sprites;
    public Sprite m_Preview;
    public Tile floorTile;
    // This refreshes itself and other RoadTiles that are orthogonally and diagonally adjacent
    public override void RefreshTile(Vector3Int location, ITilemap tilemap)
    {
        for (int yd = -1; yd <= 2; yd++)
            for (int xd = -1; xd <= 1; xd++)
            {
                Vector3Int position = new Vector3Int(location.x + xd, location.y + yd, location.z);
                if (HasRoadTile(tilemap, position))
                    tilemap.RefreshTile(position);
            }
    }
    // This determines which sprite is used based on the RoadTiles that are adjacent to it and rotates it to fit the other tiles.
    // As the rotation is determined by the RoadTile, the TileFlags.OverrideTransform is set for the tile.
    public override void GetTileData(Vector3Int location, ITilemap tilemap, ref TileData tileData)
    {
        tileData.colliderType = ColliderType.Grid;
        if(HasFloorTile(tilemap, location + Vector3Int.down))
        {
            tileData.sprite = m_Sprites[0];
            return;
        }

        if(HasFloorOrBaseTile(tilemap, location + Vector3Int.down))
        {
            if (HasFloorOrBaseTile(tilemap, location + Vector3Int.left))
            {
                tileData.sprite = m_Sprites[1];
                return;
            }
            else if (HasFloorOrBaseTile(tilemap, location + Vector3Int.right))
            {
                tileData.sprite = m_Sprites[2];
                return;
            }
            tileData.sprite = m_Sprites[3];
            return;
        }
        else if (HasFloorOrBaseTile(tilemap, location + Vector3Int.up))
        {
            if (HasFloorOrBaseTile(tilemap, location + Vector3Int.left))
            {
                tileData.sprite = m_Sprites[4];
                return;
            }
            else if (HasFloorOrBaseTile(tilemap, location + Vector3Int.right))
            {
                tileData.sprite = m_Sprites[5];
                return;
            }
            tileData.sprite = m_Sprites[6];
            return;
        }else if(HasFloorOrBaseTile(tilemap, location + Vector3Int.right))
        {
            tileData.sprite = m_Sprites[7];
            return;
        }
        else if (HasFloorOrBaseTile(tilemap, location + Vector3Int.left))
        {
            tileData.sprite = m_Sprites[8];
            return;
        }
        else if (HasFloorOrBaseTile(tilemap, location + Vector3Int.right + Vector3Int.up))
        {
            tileData.sprite = m_Sprites[9];
            return;
        }
        else if (HasFloorOrBaseTile(tilemap, location + Vector3Int.right + Vector3Int.down))
        {
            tileData.sprite = m_Sprites[10];
            return;
        }
        else if (HasFloorOrBaseTile(tilemap, location + Vector3Int.left + Vector3Int.down))
        {
            tileData.sprite = m_Sprites[11];
            return;
        }
        else if (HasFloorOrBaseTile(tilemap, location + Vector3Int.left + Vector3Int.up))
        {
            tileData.sprite = m_Sprites[12];
            return;
        }
            tileData.sprite = m_Sprites[13];
            return;
        /*int mask = HasRoadTile(tilemap, location + new Vector3Int(0, 1, 0)) ? 1 : 0;
        mask += HasRoadTile(tilemap, location + new Vector3Int(1, 0, 0)) ? 2 : 0;
        mask += HasRoadTile(tilemap, location + new Vector3Int(0, -1, 0)) ? 4 : 0;
        mask += HasRoadTile(tilemap, location + new Vector3Int(-1, 0, 0)) ? 8 : 0;
        int index = GetIndex((byte)mask);
        if (index >= 0 && index < m_Sprites.Length)
        {
            tileData.sprite = m_Sprites[index];
            tileData.color = Color.white;
            var m = tileData.transform;
            m.SetTRS(Vector3.zero, GetRotation((byte)mask), Vector3.one);
            tileData.transform = m;
            tileData.flags = TileFlags.LockTransform;
            tileData.colliderType = ColliderType.None;
        }
        else
        {
            Debug.LogWarning("Not enough sprites in RoadTile instance");
        }*/
    }
    // This determines if the Tile at the position is the same RoadTile.
    private bool HasRoadTile(ITilemap tilemap, Vector3Int position)
    {
        return tilemap.GetTile(position) == this;
    }

    private bool HasFloorOrBaseTile(ITilemap tilemap, Vector3Int position)
    {
        if(tilemap.GetTile(position) == floorTile)
        {
            return true;
        }
        return tilemap.GetTile(position + Vector3Int.down) == floorTile;
    }

    private bool HasFloorTile(ITilemap tilemap, Vector3Int position)
    {
        return tilemap.GetTile(position) == floorTile;
    }

#if UNITY_EDITOR
    // The following is a helper that adds a menu item to create a RoadTile Asset
    [MenuItem("Assets/Create/FactoryTileScript")]
    public static void CreateRoadTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save Wall Tile", "New Wall Tile", "Asset", "Save Wall Tile", "Assets");
        if (path == "")
            return;
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<FactoryTileScript>(), path);
    }
#endif
}
