using UnityEngine;

[CreateAssetMenu(fileName = "PlatformData", menuName = "Game/PlatformData")]
public class PlatformData : ScriptableObject
{
    public float jumpForce = 8f;
    public Sprite mainSprite;
    public Sprite breakLeft, breakRight;


    // Gestisce la creazione dei pezzi rotti in modo centralizzato
    public void SpawnDebris(Vector3 position)
    {
        CreatePiece(breakLeft, position + Vector3.left * 0.4f, 45f);
        CreatePiece(breakRight, position + Vector3.right * 0.4f, -45f);
    }

    private void CreatePiece(Sprite s, Vector3 pos, float rot)
    {
        if (s == null) return;
        GameObject piece = new GameObject("Debris_" + s.name);
        piece.transform.position = pos;
        piece.transform.rotation = Quaternion.Euler(0, 0, rot);
        piece.AddComponent<SpriteRenderer>().sprite = s;
        piece.AddComponent<Rigidbody2D>();
        Destroy(piece, 2f);
    }
}