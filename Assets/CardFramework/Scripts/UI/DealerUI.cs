using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DealerUI : MonoBehaviour 
{
	private Dealer _dealer;

	public Text FaceValueText { get { return _faceValueText; } }
	[SerializeField]
	private Text _faceValueText;

	private void Awake()
	{
		_dealer = GameObject.Find("Dealer").GetComponent<Dealer>();
		_dealer.DealerUIInstance = this;
	}

	public void Shuffle()
	{
		if (_dealer.DealInProgress == 0)
		{
			StartCoroutine(_dealer.ShuffleCoroutine());
		}
	}
	
	public void Draw()
	{
		if (_dealer.DealInProgress == 0)
		{
			StartCoroutine(_dealer.DrawCoroutine());
		}
	}
}
