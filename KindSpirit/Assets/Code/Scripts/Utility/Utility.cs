/* ********************************
 * Created By: Andrew Decker
 * 
 * Script that can apply to really any unity project. 
 * I've always wanted these functions so I'm finally 
 * writing them and bringing them along with me for 
 * future unity projects.
 * 
 * *******************************/

using UnityEngine;

public class Utility : MonoBehaviour
{

    // Useful for Taking a Vector3 or Vector2 and giving the closest cut vector to it (i.e. (0.7, .12, .005) returns Vector3.Left)
    #region Cut Vector

    public static Vector2 CutVector2(float x, float y)
    {
        Vector2 outputVector = Vector2.zero;

        if (x != 0 && y != 0)
        {
            if (Mathf.Abs(x) > Mathf.Abs(y))
                outputVector = x > 0 ? Vector2.right : Vector2.left;
            else
                outputVector = y > 0 ? Vector2.up : Vector2.down;
        }

        return outputVector;
    }

    public static Vector2 CutVector2(Vector2 inputVector)
    {
        Vector2 outputVector = Vector2.zero;

        if (inputVector.x != 0 && inputVector.y != 0)
        {
            if (Mathf.Abs(inputVector.x) > Mathf.Abs(inputVector.y))
                outputVector = inputVector.x > 0 ? Vector2.right : Vector2.left;
            else
                outputVector = inputVector.y > 0 ? Vector2.up : Vector2.down;
        }

        return outputVector;
    }



    #endregion

    // Useful for Giving some cheap and fast variation on sound effects
    #region Audio Source Manipulation

    public static void FlourishAudioSource(AudioSource source)
    {
        source.pitch = Random.Range(0.8f, 1.2f);
        source.volume = Random.Range(0.8f, 1.0f);
    }

    public static void FlourishAudioSource(AudioSource source, float pitchMin, float pitchMax, float volMin, float volMax)
    {
        source.pitch = Random.Range(pitchMin, pitchMax);
        source.volume = Random.Range(volMin, volMax);
    }

    #endregion

    // Gives back random values for variable types not included in Random Class
    #region Random Variable Values

    public static bool RandomBool()
    {
        if (Random.value > 0.5)
            return true;
        return false;
    }

    public static Vector2 RandomVector2()
    {
        Vector2 randomVector = new Vector2(Random.value, Random.value);
        randomVector.Normalize();
        return randomVector;
    }

    public static Vector3 RandomVector3()
    {
        Vector3 randomVector = new Vector3(Random.value, Random.value, Random.value);
        randomVector.Normalize();
        return randomVector;
    }

    #endregion

    // Useful for making switch functions using Vectors. Takes in a Vector and returns the enum or vice versa.
    #region Directional Enums

    public enum Directions
    {
        Zero,
        Up,
        Down,
        Left,
        Right,
        Forward,
        Back,
    }

    public static Vector3 TranslateDirectionEnumToVector(Directions dir)
    {
        switch (dir)
        {
            case Directions.Up:
                return Vector3.up;
            case Directions.Down:
                return Vector3.down;
            case Directions.Left:
                return Vector3.left;
            case Directions.Right:
                return Vector3.right;
            case Directions.Forward:
                return Vector3.forward;
            case Directions.Back:
                return Vector3.back;
            case Directions.Zero:
                return Vector3.zero;
        }
        return Vector2.zero;
    }

    public static Vector2 TranslateDirectionEnumToVector2(Directions dir)
    {
        switch (dir)
        {
            case Directions.Up:
                return Vector2.up;
            case Directions.Down:
                return Vector2.down;
            case Directions.Left:
                return Vector2.left;
            case Directions.Right:
                return Vector2.right;
            case Directions.Zero:
                return Vector2.zero;
        }
        return Vector2.zero;
    }

    //public Directions TranslateVector3ToEnum(Vector3)

    #endregion
}
