using TMPro;
using UnityEngine;

/// <summary>
/// Create your validator class and inherit TMPro.TMP_InputValidator 
/// Note that this is a ScriptableObject, so you'll have to create an instance of it via the Assets -> Create -> Input Field Validator 
/// </summary>
[CreateAssetMenu(fileName = "Input Field Validator", menuName = "Input Field Validator")]
public class TMPFloatValidator : TMP_InputValidator
{
    [SerializeField] private bool canBeNegative = false;
    /// <summary>
    /// Override Validate method to implement your own validation
    /// </summary>
    /// <param name="text">This is a reference pointer to the actual text in the input field; changes made to this text argument will also result in changes made to text shown in the input field</param>
    /// <param name="pos">This is a reference pointer to the input field's text insertion index position (your blinking caret cursor); changing this value will also change the index of the input field's insertion position</param>
    /// <param name="ch">This is the character being typed into the input field</param>
    /// <returns>Return the character you'd allow into </returns>
    public override char Validate(ref string text, ref int pos, char ch)
    {
        Debug.Log($"Text = {text}; pos = {pos}; chr = {ch}");
        // If the typed character is a number or we enter dot first time, insert it into the text argument at the text insertion position (pos argument)
        if (char.IsNumber(ch))
        {
            // Insert the character at the given position if we're working in the Unity Editor
            text = text.Insert(pos, ch.ToString());
            // Increment the insertion point by 1
            pos++;
            return '\0';
        }
        else if (ch == '.')
        {
            Debug.Log(text);
            int dotIndex = text.IndexOf('.');
            if (dotIndex == -1)
            {
                if (pos == 0)
                {
                    text = text.Insert(pos, "0.");
                    pos += 2;
                    return ch;
                }

                text = text.Insert(pos, ".");
                pos++;
                return '\0';
            }
        }
        else if (canBeNegative && ch == '-' && pos == 0)
        {
            text = text.Insert(pos, "-");
            ++pos;
            return '\0';
        }
        // If the character is not a number, return null
        return '\0';
    }
}
