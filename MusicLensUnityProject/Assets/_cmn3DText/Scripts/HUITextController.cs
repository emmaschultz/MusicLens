using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CWRU.Common.Text
{
	/// <summary>
	/// Horizontal Alignment for 3DText
	/// </summary>
	public enum HUITextHorizontalAlignment
	{
		Left,
		Center,
		Right
	}

    /// <summary>
	/// Vertical Alignment for 3DText
	/// </summary>
	public enum HUITextVerticalAlignment
    {
        Top,
        Center,
        Bottom
    }

    /// <summary>
	/// Scale Type for 3DText
	/// </summary>
	public enum ScaleType
    {
        Normal,
        Bounded
    }

    /// <summary>
    /// Creates necessary child objects for characters set in SetText
    /// </summary>
    public class HUITextController : MonoBehaviour
    {
        public bool DisplayInitialText = false;
        public string InitialText = "";

        [Header("Scaling")]
        public ScaleType scaleType;
		public float DefaultScale = 1.0f;
        public float MaxScale = 1f;

        [Header("Alignment")]
		public HUITextHorizontalAlignment HorizontalAlignment = HUITextHorizontalAlignment.Left;
        public HUITextVerticalAlignment VerticalAlignment = HUITextVerticalAlignment.Top;

        [Header("Letter Prefabs")]
        public GameObject space;
		public GameObject period;
		public GameObject backslash;

		public GameObject zero;
		public GameObject one;
		public GameObject two;
		public GameObject three;
		public GameObject four;
		public GameObject five;
		public GameObject six;
		public GameObject seven;
		public GameObject eight;
		public GameObject nine;

		public GameObject a_lower;
		public GameObject A_UPPER;
		public GameObject b_lower;
		public GameObject B_UPPER;
		public GameObject c_lower;
		public GameObject C_UPPER;
		public GameObject d_lower;
		public GameObject D_UPPER;
		public GameObject e_lower;
		public GameObject E_UPPER;
		public GameObject f_lower;
		public GameObject F_UPPER;
		public GameObject g_lower;
		public GameObject G_UPPER;
		public GameObject h_lower;
		public GameObject H_UPPER;
		public GameObject i_lower;
		public GameObject I_UPPER;
		public GameObject j_lower;
		public GameObject J_UPPER;
		public GameObject k_lower;
		public GameObject K_UPPER;
		public GameObject l_lower;
		public GameObject L_UPPER;
		public GameObject m_lower;
		public GameObject M_UPPER;
		public GameObject n_lower;
		public GameObject N_UPPER;
		public GameObject o_lower;
		public GameObject O_UPPER;
		public GameObject p_lower;
		public GameObject P_UPPER;
		public GameObject q_lower;
		public GameObject Q_UPPER;
		public GameObject r_lower;
		public GameObject R_UPPER;
		public GameObject s_lower;
		public GameObject S_UPPER;
		public GameObject t_lower;
		public GameObject T_UPPER;
		public GameObject u_lower;
		public GameObject U_UPPER;
		public GameObject v_lower;
		public GameObject V_UPPER;
		public GameObject w_lower;
		public GameObject W_UPPER;
		public GameObject x_lower;
		public GameObject X_UPPER;
		public GameObject y_lower;
		public GameObject Y_UPPER;
		public GameObject z_lower;
		public GameObject Z_UPPER;

		private List<GameObject> letters = new List<GameObject>();
		private GameObject huiTextChild;
        private float endX;

		// Use this for initialization
		void Awake()
		{
			if (DisplayInitialText)
				SetText(InitialText);
		}

		private void addNewLetter(GameObject letterPrefab, float startX, Vector3 startPos, out float newStartX, out Vector3 newStartPos, float offset)
		{

			GameObject newLetter = (GameObject)GameObject.Instantiate(letterPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);

			BoxCollider boxCollider = newLetter.GetComponentInChildren<BoxCollider>();

			startX = startX + boxCollider.bounds.max.x + offset;

			newLetter.transform.position = startPos;
			newLetter.transform.rotation = huiTextChild.transform.rotation; //this.transform.rotation;

			newLetter.transform.parent = huiTextChild.transform; // this.transform;

			startPos = huiTextChild.transform.right * startX + huiTextChild.transform.position; //this.transform.right * startX + this.transform.position;

			newStartX = startX;
			newStartPos = startPos;

			letters.Add(newLetter);
			boxCollider.enabled = false;
		}

		/// <summary>
		/// Creates 3D text
		/// </summary>
		/// <param name="text">The text to create</param>
		public void SetText(string text)
		{
			SetText(text, DefaultScale);
		}

		/// <summary>
		/// Creates 3D text
		/// </summary>
		/// <param name="text">The text to create</param>
		/// <param name="scale">The scale at which to create the text</param>
		public void SetText(string text, float scale)
		{
			foreach (GameObject letter in letters)
			{
				GameObject.Destroy(letter);
			}
			letters.Clear();

			if (huiTextChild != null)
			{
				GameObject.Destroy(huiTextChild);
			}

			huiTextChild = new GameObject("huiTextChild");
			huiTextChild.transform.position = new Vector3(0f, 0f, 0f);
			huiTextChild.transform.rotation = Quaternion.identity;
			huiTextChild.transform.localScale = new Vector3(1f, 1f, 1f);

			float startX = 0;
			Vector3 startPos = huiTextChild.transform.position;//this.transform.position;

			char[] chars = text.ToCharArray();
			for (int i = 0; i < text.Length; i++)
			{
				char c = chars[i];

				switch (c)
				{
					case ' ':
						addNewLetter(space, startX, startPos, out startX, out startPos, 0f);
						break;
					case '.':
						addNewLetter(period, startX, startPos, out startX, out startPos, 0f);
						break;
					case '\\':
						addNewLetter(backslash, startX, startPos, out startX, out startPos, 0f);
						break;

					case '0':
						addNewLetter(zero, startX, startPos, out startX, out startPos, 0f);
						break;
					case '1':
						addNewLetter(one, startX, startPos, out startX, out startPos, 0f);
						break;
					case '2':
						addNewLetter(two, startX, startPos, out startX, out startPos, 0f);
						break;
					case '3':
						addNewLetter(three, startX, startPos, out startX, out startPos, 0f);
						break;
					case '4':
						addNewLetter(four, startX, startPos, out startX, out startPos, 0f);
						break;
					case '5':
						addNewLetter(five, startX, startPos, out startX, out startPos, 0f);
						break;
					case '6':
						addNewLetter(six, startX, startPos, out startX, out startPos, 0f);
						break;
					case '7':
						addNewLetter(seven, startX, startPos, out startX, out startPos, 0f);
						break;
					case '8':
						addNewLetter(eight, startX, startPos, out startX, out startPos, 0f);
						break;
					case '9':
						addNewLetter(nine, startX, startPos, out startX, out startPos, 0f);
						break;

					case 'a':
						addNewLetter(a_lower, startX, startPos, out startX, out startPos, 0f);
						break;
					case 'A':
						addNewLetter(A_UPPER, startX, startPos, out startX, out startPos, 0f);
						break;
					case 'b':
						addNewLetter(b_lower, startX, startPos, out startX, out startPos, 0f);
						break;
					case 'B':
						addNewLetter(B_UPPER, startX, startPos, out startX, out startPos, 0f);
						break;
					case 'c':
						addNewLetter(c_lower, startX, startPos, out startX, out startPos, 0f);
						break;
					case 'C':
						addNewLetter(C_UPPER, startX, startPos, out startX, out startPos, 0f);
						break;
					case 'd':
						addNewLetter(d_lower, startX, startPos, out startX, out startPos, 0f);
						break;
					case 'D':
						addNewLetter(D_UPPER, startX, startPos, out startX, out startPos, 0f);
						break;
					case 'e':
						addNewLetter(e_lower, startX, startPos, out startX, out startPos, 0f);
						break;
					case 'E':
						addNewLetter(E_UPPER, startX, startPos, out startX, out startPos, 0f);
						break;
					case 'f':
						addNewLetter(f_lower, startX, startPos, out startX, out startPos, 0f);
						break;
					case 'F':
						addNewLetter(F_UPPER, startX, startPos, out startX, out startPos, 0f);
						break;
					case 'g':
						addNewLetter(g_lower, startX, startPos, out startX, out startPos, 0f);
						break;
					case 'G':
						addNewLetter(G_UPPER, startX, startPos, out startX, out startPos, 0f);
						break;
					case 'h':
						addNewLetter(h_lower, startX, startPos, out startX, out startPos, 0f);
						break;
					case 'H':
						addNewLetter(H_UPPER, startX, startPos, out startX, out startPos, 0f);
						break;
					case 'i':
						addNewLetter(i_lower, startX, startPos, out startX, out startPos, 0f);
						break;
					case 'I':
						addNewLetter(I_UPPER, startX, startPos, out startX, out startPos, 0f);
						break;
					case 'j':
						addNewLetter(j_lower, startX, startPos, out startX, out startPos, 0f);
						break;
					case 'J':
						addNewLetter(J_UPPER, startX, startPos, out startX, out startPos, 0f);
						break;
					case 'k':
						addNewLetter(k_lower, startX, startPos, out startX, out startPos, 0f);
						break;
					case 'K':
						addNewLetter(K_UPPER, startX, startPos, out startX, out startPos, 0f);
						break;
					case 'l':
						addNewLetter(l_lower, startX, startPos, out startX, out startPos, 0f);
						break;
					case 'L':
						addNewLetter(L_UPPER, startX, startPos, out startX, out startPos, 0f);
						break;
					case 'm':
						addNewLetter(m_lower, startX, startPos, out startX, out startPos, 0f);
						break;
					case 'M':
						addNewLetter(M_UPPER, startX, startPos, out startX, out startPos, 0f);
						break;
					case 'n':
						addNewLetter(n_lower, startX, startPos, out startX, out startPos, 0f);
						break;
					case 'N':
						addNewLetter(N_UPPER, startX, startPos, out startX, out startPos, 0f);
						break;
					case 'o':
						addNewLetter(o_lower, startX, startPos, out startX, out startPos, 0f);
						break;
					case 'O':
						addNewLetter(O_UPPER, startX, startPos, out startX, out startPos, 0f);
						break;
					case 'p':
						addNewLetter(p_lower, startX, startPos, out startX, out startPos, 0f);
						break;
					case 'P':
						addNewLetter(P_UPPER, startX, startPos, out startX, out startPos, 0f);
						break;
					case 'q':
						addNewLetter(q_lower, startX, startPos, out startX, out startPos, 0f);
						break;
					case 'Q':
						addNewLetter(Q_UPPER, startX, startPos, out startX, out startPos, 0f);
						break;
					case 'r':
						addNewLetter(r_lower, startX, startPos, out startX, out startPos, 0f);
						break;
					case 'R':
						addNewLetter(R_UPPER, startX, startPos, out startX, out startPos, 0f);
						break;
					case 's':
						addNewLetter(s_lower, startX, startPos, out startX, out startPos, 0f);
						break;
					case 'S':
						addNewLetter(S_UPPER, startX, startPos, out startX, out startPos, 0f);
						break;
					case 't':
						addNewLetter(t_lower, startX, startPos, out startX, out startPos, 0f);
						break;
					case 'T':
						addNewLetter(T_UPPER, startX, startPos, out startX, out startPos, 0f);
						break;
					case 'u':
						addNewLetter(u_lower, startX, startPos, out startX, out startPos, 0f);
						break;
					case 'U':
						addNewLetter(U_UPPER, startX, startPos, out startX, out startPos, 0f);
						break;
					case 'v':
						addNewLetter(v_lower, startX, startPos, out startX, out startPos, 0f);
						break;
					case 'V':
						addNewLetter(V_UPPER, startX, startPos, out startX, out startPos, 0f);
						break;
					case 'w':
						addNewLetter(w_lower, startX, startPos, out startX, out startPos, 0f);
						break;
					case 'W':
						addNewLetter(W_UPPER, startX, startPos, out startX, out startPos, 0f);
						break;
					case 'x':
						addNewLetter(x_lower, startX, startPos, out startX, out startPos, 0f);
						break;
					case 'X':
						addNewLetter(X_UPPER, startX, startPos, out startX, out startPos, 0f);
						break;
					case 'y':
						addNewLetter(y_lower, startX, startPos, out startX, out startPos, 0f);
						break;
					case 'Y':
						addNewLetter(Y_UPPER, startX, startPos, out startX, out startPos, 0f);
						break;
					case 'z':
						addNewLetter(z_lower, startX, startPos, out startX, out startPos, 0f);
						break;
					case 'Z':
						addNewLetter(Z_UPPER, startX, startPos, out startX, out startPos, 0f);
						break;

					default:
						break;
				}
                endX = startX;
            }
            ReallignText();


        }

        void ReallignText()
        {
            if (!this.huiTextChild)
                return;

            switch (scaleType)
            {
                case ScaleType.Normal:
                    this.huiTextChild.transform.localScale = this.transform.localScale;
                    this.huiTextChild.transform.localScale = this.huiTextChild.transform.localScale * DefaultScale;
                    break;
                case ScaleType.Bounded:
                    float scaleValue = Mathf.Min(MaxScale, DefaultScale / endX);
                    this.huiTextChild.transform.localScale = this.transform.localScale * scaleValue;
                    break;
            }

            switch (HorizontalAlignment)
            {
                case HUITextHorizontalAlignment.Left:
                    this.huiTextChild.transform.position = this.transform.position;
                    break;
                case HUITextHorizontalAlignment.Center:
                    this.huiTextChild.transform.position = new Vector3(this.transform.position.x - endX / 2.0f * this.huiTextChild.transform.localScale.x, this.transform.position.y, this.transform.position.z);
                    break;
                case HUITextHorizontalAlignment.Right:
                    this.huiTextChild.transform.position = new Vector3(this.transform.position.x - endX * this.huiTextChild.transform.localScale.x, this.transform.position.y, this.transform.position.z);
                    break;
                default:
                    break;
            }

            switch (VerticalAlignment)
            {
                case HUITextVerticalAlignment.Top:
                    this.huiTextChild.transform.position = this.huiTextChild.transform.position;
                    break;
                case HUITextVerticalAlignment.Center:
                    this.huiTextChild.transform.position = new Vector3(this.huiTextChild.transform.position.x,
                        this.huiTextChild.transform.position.y - this.huiTextChild.transform.localScale.y * 4f/3f,
                        this.huiTextChild.transform.position.z);
                    break;
                case HUITextVerticalAlignment.Bottom:
                    this.huiTextChild.transform.position = new Vector3(this.huiTextChild.transform.position.x,
                        this.huiTextChild.transform.position.y - this.huiTextChild.transform.localScale.y * 8f/3f,
                        this.huiTextChild.transform.position.z);
                    break;
                default:
                    break;
            }

            this.huiTextChild.transform.rotation = this.transform.localRotation;

            this.huiTextChild.transform.parent = this.transform;
        }
    }

    

}