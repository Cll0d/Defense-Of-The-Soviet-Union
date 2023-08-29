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


	LoadExtern: function (){
		player.getData().then(_date => {
			const myJSON = JSON.stringify(_date);
			myGameInstance.SendMessage('Progress', 'Load' , myJSON);
		});
	},

	
	GetLang : function() {
		var lang = ysdk.enviroment.i18n.lang;
		var bufferSize = lengthBytesUTF8(lang) + 1;
		var buffer = _malloc(bufferSize);
		stringToUTF8(lang,buffer,bufferSize);
		return buffer;
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


});