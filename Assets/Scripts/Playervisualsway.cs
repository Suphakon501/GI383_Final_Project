using UnityEngine;

/// <summary>
/// PlayerVisualSway.cs
/// ★ ไม่แตะ position เลย — แตะแค่ rotation Z และ scale
///    ทำให้ไม่ขัดกับ PlayerController (ซ้ายขวา) และ SliderZoneEffect (ขึ้น)
/// </summary>
public class PlayerVisualSway : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Transform ของ Sprite ลูก ถ้าปล่อยว่างจะใช้ตัวเอง")]
    public Transform visualTransform;

    // ── Tilt ──────────────────────────────────────
    [Header("Sway / Tilt")]
    public float maxTiltAngle = 18f;
    public float tiltSpeed = 8f;
    public float tiltReturnSpeed = 5f;

    // ── Squash & Stretch ──────────────────────────
    [Header("Squash & Stretch")]
    [Tooltip("+% scale X ตอนขยับ เช่น 1.12 = บวก 12% จาก scale เดิม")]
    public float stretchXMult = 1.12f;
    [Tooltip("-% scale Y ตอนขยับ เช่น 0.90 = ลด 10% จาก scale เดิม")]
    public float stretchYMult = 0.90f;
    public float squashSpeed = 10f;

    // ── Private ───────────────────────────────────
    private float currentTilt = 0f;
    private float inputDir = 0f;
    private Vector3 baseLocalScale;

    void Start()
    {
        if (visualTransform == null)
            visualTransform = transform;

        baseLocalScale = visualTransform.localScale;
    }

    void Update()
    {
        ReadInput();
        UpdateTilt();
        UpdateSquashStretch();
        // ★ ไม่มี UpdatePosition / UpdateWobble — ไม่แตะ position เลย
    }

    void ReadInput()
    {
        inputDir = 0f;
        if (Input.GetKey(KeyCode.A)) inputDir = -1f;
        if (Input.GetKey(KeyCode.D)) inputDir = 1f;
    }

    void UpdateTilt()
    {
        float targetTilt = -inputDir * maxTiltAngle;
        float speed = (inputDir != 0f) ? tiltSpeed : tiltReturnSpeed;
        currentTilt = Mathf.Lerp(currentTilt, targetTilt, Time.deltaTime * speed);

        Quaternion rot = visualTransform.localRotation;
        visualTransform.localRotation = Quaternion.Euler(
            rot.eulerAngles.x, rot.eulerAngles.y, currentTilt);
    }

    void UpdateSquashStretch()
    {
        float t = Mathf.Abs(inputDir);
        float targetX = baseLocalScale.x * Mathf.Lerp(1f, stretchXMult, t);
        float targetY = baseLocalScale.y * Mathf.Lerp(1f, stretchYMult, t);

        Vector3 cur = visualTransform.localScale;
        visualTransform.localScale = new Vector3(
            Mathf.Lerp(cur.x, targetX, Time.deltaTime * squashSpeed),
            Mathf.Lerp(cur.y, targetY, Time.deltaTime * squashSpeed),
            cur.z
        );
    }
}