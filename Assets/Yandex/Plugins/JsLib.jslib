mergeInto(LibraryManager.library, {

  ShowFullscreenAdv: function () {
    ysdk.adv.showFullscreenAdv({
    callbacks: {
        onClose: function(wasShown) {
          // some action after close
        },
        onError: function(error) {
          // some action on error
        }
      }
    })
  },

 GetLanguage: function () {
    var returnStr = ysdk.environment.i18n.lang;
    var bufferSize = lengthBytesUTF8(returnStr) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(returnStr, buffer, bufferSize);
    return buffer;
 },

 InitLeaderboard: function(){
    InitLeaderboard();
 },

 SetLeaderboardScores: function (nameLB, score){
    var nameLBO = UTF8ToString(nameLB);
    var scores = score;
    SetLeaderboard(nameLBO, scores);
 },

});