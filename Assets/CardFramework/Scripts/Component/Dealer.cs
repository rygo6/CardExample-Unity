using UnityEngine;
using System.Collections;
using System.IO;

public class Dealer : MonoBehaviour 
{
	public DealerUI DealerUIInstance { get; set; }
    
	[SerializeField]
	private CardDeck _cardDeck;	

	[SerializeField]
	private CardSlot _pickupCardSlot;		

	[SerializeField]
	private CardSlot _stackCardSlot;	

	[SerializeField]
	private CardSlot _discardStackCardSlot;		

	[SerializeField]
	private CardSlot _discardHoverStackCardSlot;			

	[SerializeField]
	private CardSlot _rightHandCardSlot;

	[SerializeField]
	private CardSlot _leftHandCardSlot;

	[SerializeField]
	private CardSlot _currentCardSlot;	

	[SerializeField]
	private CardSlot _prior0CardSlot;	

	[SerializeField]
	private CardSlot _prior1CardSlot;	

	[SerializeField]
	private CardSlot _prior2CardSlot;		

	[SerializeField]
	private CardSlot _prior3CardSlot;

	[SerializeField]
	private CardSlot _prior4CardSlot;																																								

	private const float CardStackDelay = .01f;
	
	/// <summary>
	/// Counter which keeps track current dealing movements in progress.
	/// </summary>
	public int DealInProgress { get; set; }

	private void Awake()
	{
		_cardDeck.InstanatiateDeck("cards");
		StartCoroutine(StackCardRangeOnSlot(0, _cardDeck.CardList.Count, _stackCardSlot));
	}
    
	private void MoveCardSlotToCardSlot(CardSlot sourceCardSlot, CardSlot targerCardSlot) 
	{
		Card card;
		while ((card = sourceCardSlot.TopCard()) != null)
		{
			targerCardSlot.AddCard(card);
		}
	}
	
	private IEnumerator StackCardRangeOnSlot(int start, int end, CardSlot cardSlot) 
	{
		DealInProgress++;
		for (int i = start; i < end; ++i)
		{
			cardSlot.AddCard(_cardDeck.CardList[i]);
			yield return new WaitForSeconds(CardStackDelay);
		}
		DealInProgress--;
	}
    
    /// <summary>
    /// Shuffle Coroutine.
    /// Moves all card to pickupCardSlot. Then shuffles them back
	/// to cardStackSlot.
    /// </summary>
	public IEnumerator ShuffleCoroutine()
	{
		DealInProgress++;
		DealerUIInstance.FaceValueText.text = "0";
		MoveCardSlotToCardSlot(_stackCardSlot, _pickupCardSlot);		
		MoveCardSlotToCardSlot(_prior0CardSlot, _pickupCardSlot);
		MoveCardSlotToCardSlot(_prior1CardSlot, _pickupCardSlot);	
		MoveCardSlotToCardSlot(_prior2CardSlot, _pickupCardSlot);		
		MoveCardSlotToCardSlot(_prior3CardSlot, _pickupCardSlot);	
		MoveCardSlotToCardSlot(_prior4CardSlot, _pickupCardSlot);	
		MoveCardSlotToCardSlot(_discardStackCardSlot, _pickupCardSlot);
		MoveCardSlotToCardSlot(_currentCardSlot, _pickupCardSlot);			
		yield return new WaitForSeconds(.4f);	
		int halfLength = _cardDeck.CardList.Count / 2;
		for (int i = 0; i < halfLength; ++i)
		{
			_leftHandCardSlot.AddCard(_pickupCardSlot.TopCard());
		}
		yield return new WaitForSeconds(.2f);	
		for (int i = 0; i < halfLength; ++i)
		{
			_rightHandCardSlot.AddCard(_pickupCardSlot.TopCard());
		}
		yield return new WaitForSeconds(.2f);	
		for (int i = 0; i < _cardDeck.CardList.Count; ++i)
		{
			if (i % 2 == 0)
			{
				_stackCardSlot.AddCard(_rightHandCardSlot.TopCard());
			}
			else
			{
				_stackCardSlot.AddCard(_leftHandCardSlot.TopCard());
			}
			yield return new WaitForSeconds(CardStackDelay);
		}
		DealInProgress--;
    }

	public IEnumerator DrawCoroutine()
	{
		DealInProgress++;
		
		if (_discardHoverStackCardSlot.AddCard(_prior4CardSlot.TopCard()))
		{	
			yield return new WaitForSeconds(CardStackDelay);	
		}	
		if (_discardStackCardSlot.AddCard(_discardHoverStackCardSlot.TopCard()))
		{
			yield return new WaitForSeconds(CardStackDelay);
		}
		if (_prior4CardSlot.AddCard(_prior3CardSlot.TopCard()))
		{
			yield return new WaitForSeconds(CardStackDelay);
		}
		if (_prior3CardSlot.AddCard(_prior2CardSlot.TopCard()))
		{
			yield return new WaitForSeconds(CardStackDelay);
		}
		if (_prior2CardSlot.AddCard(_prior1CardSlot.TopCard()))
		{
			yield return new WaitForSeconds(CardStackDelay);
		}
		if (_prior1CardSlot.AddCard(_prior0CardSlot.TopCard()))
		{
			yield return new WaitForSeconds(CardStackDelay);	
		}
		if (_prior0CardSlot.AddCard(_currentCardSlot.TopCard()))
		{
			yield return new WaitForSeconds(CardStackDelay);		
		}		
		_currentCardSlot.AddCard(_stackCardSlot.TopCard());	
		
		int collectiveFaceValue = _prior0CardSlot.FaceValue();
		collectiveFaceValue += _prior1CardSlot.FaceValue();
		collectiveFaceValue += _prior2CardSlot.FaceValue();
		collectiveFaceValue += _prior3CardSlot.FaceValue();
		collectiveFaceValue += _prior4CardSlot.FaceValue();
		collectiveFaceValue += _currentCardSlot.FaceValue();	
		DealerUIInstance.FaceValueText.text = collectiveFaceValue.ToString();
		
		DealInProgress--;
	}	
}
