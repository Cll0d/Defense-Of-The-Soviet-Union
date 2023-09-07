mergeInto(LibraryManager.library, {
	
	Hello: function (){
		window.alert("Hello world!");
		console.log("Hello world!");
	},

	RateGame: function (){
		ysdk.feedback.canReview()
        .then(({ value, reason }) => {
            if (value) {
                ysdk.feedback.requestReview()
                    .then(({ feedbackSent }) => {
                        console.log(feedbackSent);
                    })
            } else {
                console.log(reason)
            }
        })
	},

	SaveExtern: function (date){
		var dateString = UTF8ToString(date);
		var myobj = JSON.parse(dateString);
		player.setData(myobj);
	},


	//

	LoadExtern: function(){
    if(player != null){
      player.getData().then(_date => {
          const myJSON = JSON.stringify(_date);
          myGameInstance.SendMessage('Progress', 'Load', myJSON);
      });
    }
    else{
      LoadExtern();
    }
  },


	ShowAdv : function(){
		ysdk.adv.showFullscreenAdv({
			callbacks:{
			onClose:function(wasShown) {
				console.log("-------------closed-----------")
			},
			onError : function (error){
				
			}
			}
		})
	},

	AddCoins : function(value){
		ysdk.adv.showRewardedVideo({
    		callbacks: {
        onOpen: () => {
          console.log('Video ad open.');
        },
        onRewarded: () => {
          console.log('Rewarded!');
          myGameInstance.SendMessage("CoinManager", "AddCoinsInGame", value);
        },
        onClose: () => {
          console.log('Video ad closed.');
        }, 
        onError: (e) => {
          console.log('Error while open video ad:', e);
        }
    }
})
	}
});