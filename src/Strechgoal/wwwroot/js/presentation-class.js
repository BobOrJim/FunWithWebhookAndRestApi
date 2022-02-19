export class Presentation{
  constructor(httpService) {
    this.httpService = httpService
    this.myDivBoardId = document.getElementById("myDivBoardId")
    this.asyncPerpetualTimer1000ms()
  }

  scoreBoard(){ //requesting data from backend, and updating the scoreBoard
    this.httpService.recieve()
    let scoreBoardData = this.httpService.dataFromBackend
    let scoreBoardTable = document.getElementById("scoreBoard")
    this.clearTable(scoreBoardTable)
    //console.log(scoreBoardData)
    if (!(scoreBoardData == undefined)){
      scoreBoardData.sort((a, b) => a.time - b.time)
      scoreBoardData = scoreBoardData.slice(0, 10);
      scoreBoardData.reverse()
      for (let i=0; i < Math.min(scoreBoardData.length, 10); i++ ){
        this.addRowToScoreBoard(scoreBoardData.length - i, scoreBoardData[i].name, scoreBoardData[i].time)
      }
    }
  }

  //Remove all rows
  clearTable(table) {
    var rows = table.rows;
    var i = rows.length;
    while (--i) {
      rows[i].parentNode.removeChild(rows[i]);
    }
  }

  addRowToScoreBoard(position, name, time){
    let scoreBoardTable = document.getElementById("scoreBoard")
    let row = scoreBoardTable.insertRow(1);

    // Insert new cells (<td>)  in row above
    let positionCell = row.insertCell(0);
    let nameCell = row.insertCell(1);
    let timeCell = row.insertCell(2);

    // Add some text to the new cells:
    positionCell.innerHTML = position
    nameCell.innerHTML = name
    timeCell.innerHTML = time
  }

  //setInterval didnt do the trick when calling this.timePassed(). Problems with scope i think.
  //So i had to do a workaround with async/await. Where the setIntervall is just a dummy.
  asyncPerpetualTimer1000ms = async () => {
    await new Promise((dummy) => setInterval(dummy, 2000)); //dummy to pause function before calling itself again.
    this.scoreBoard()
    this.asyncPerpetualTimer1000ms()
  }

}