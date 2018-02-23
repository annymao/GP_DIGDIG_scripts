using System.Collections.Generic;
using UnityEngine;

public class RoomLayoutGroup : MonoBehaviour {

	[SerializeField] private GameObject _roomListingPrefab;
	private GameObject RoomListingPrefab { get { return _roomListingPrefab;  } }

	private List<RoomListing> _roomListingButtons = new List<RoomListing>();
	private List<RoomListing> RoomListingButtons { get { return _roomListingButtons; } }

	// Photon network check the room update
	private void OnReceivedRoomListUpdate() {
		RoomInfo[] rooms = PhotonNetwork.GetRoomList();

		foreach (RoomInfo room in rooms) {
			RoomReceived(room); 
		}

		RemoveOldRooms();
	}

	// Photn network check the new room whether has existed or not when CreateRoom() is called
	private void RoomReceived(RoomInfo room) {
		int index = RoomListingButtons.FindIndex(x => x.RoomName == room.Name);

		// Add a room
		if (index == -1) {
			if (room.IsVisible && room.PlayerCount < room.MaxPlayers) {
				GameObject roomListingObj = Instantiate(RoomListingPrefab);
				roomListingObj.transform.SetParent(this.transform, false);

				RoomListing roomListing = roomListingObj.GetComponent<RoomListing>();
				RoomListingButtons.Add(roomListing);
				
				index = (RoomListingButtons.Count - 1);
			}
		}

		// Room has already existed or has created 
		if (index != -1) {
			RoomListing roomListing = RoomListingButtons[index];
			roomListing.SetRoomNameText(room.Name);
			roomListing.Updated = true;


		}
	}

	// Photon network delete the room when the room connection is off
	private void RemoveOldRooms() {
		List<RoomListing> removeRooms = new List<RoomListing>();

		foreach (RoomListing roomListing in RoomListingButtons) {
			if (!roomListing.Updated) {
				removeRooms.Add(roomListing);
			}
			else {
				roomListing.Updated = false;
			}
		}

		foreach (RoomListing roomListing in removeRooms) {
			GameObject roomListingObj = roomListing.gameObject;
			RoomListingButtons.Remove(roomListing);
			Destroy(roomListingObj);
		}

	}
}
