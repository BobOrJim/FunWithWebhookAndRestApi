
export class StopWatch{
    constructor(){
        this.startTime = 0
        this.endTime = 0
        this.duration = 0
        this.running = false
    }

    start(){
        console.log("start stopwatch")
        if (this.running)
            throw new Error("Stopwatch is already started")
        this.running = true
        this.startTime = new Date()
    }

    stop(){
        console.log("stop stopwatch")
        if(!this.running)
            throw new Error("Stopwatch is not started")
        this.running = false
        this.endTime = new Date()
        const seconds = (this.endTime.getTime() - this.startTime.getTime()) / 1000
        this.duration += seconds
    }

    reset(){
        console.log("reset stopwatch")
        this.startTime = new Date()
        this.stopTime = new Date()
        this.duration = 0
        this.running = false
    }

    getDuration(){
        //console.log("duration")
        this.endTime = new Date()
        const seconds = (this.endTime.getTime() - this.startTime.getTime()) / 1000
        if (this.running){
            this.startTime = new Date()
            this.duration += seconds
        }
        return this.duration.toFixed(3)
    }
}


