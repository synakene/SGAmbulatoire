/**
 * Scorm constructor
 */
function Scorm(version) {
	if (typeof version == "undefined") {
		this.version = this.SCORM_1_2;
	} else if (version == this.SCORM_1_2 || version == this.SCORM_2004) {
		this.version = version;
	} else {
		this.logError("Version not valid: " + version);
	}
}

// Constants
Scorm.prototype.SCORM_TRUE = "true";
Scorm.prototype.SCORM_FALSE = "false";
Scorm.prototype.SCORM_NO_ERROR = "0";

Scorm.prototype.SCORM_1_2 = "Scorm 1.2";
Scorm.prototype.SCORM_2004 = "Scorm 2004";

// Flags
Scorm.prototype.initialized = false;
Scorm.prototype.finishCalled = false;

// Misc
Scorm.prototype.version = null;
Scorm.prototype.API = null;
Scorm.prototype.findAPITries = 0;
Scorm.prototype.maxAPITries = 10;

/**
 * Find API.
 */
Scorm.prototype.findAPI = function(win) {
   while ( (this.version == this.SCORM_1_2 ? win.API == null : win.API_1484_11 == null) &&
           (win.parent != null) &&
           (win.parent != win) ) {

		this.findAPITries++;

		if (this.findAPITries > this.maxAPITries) {
			this.logError("Error finding API, too deeply nested.");
			return null;
		}
		
		win = win.parent;
   }
   
   return this.version == this.SCORM_1_2 ? win.API : win.API_1484_11;
}

/**
 * Get API.
 */
Scorm.prototype.getAPI = function() {
   var theAPI = this.findAPI(window);

   if ( (theAPI == null) &&
        (window.opener != null) &&
        (typeof(window.opener) != "undefined") ) {
      theAPI = this.findAPI(window.opener);
   }

   if (theAPI == null) {
      this.logError("Unable to find an API adapter");
   }
   
   return theAPI;
}

/**
 * Begins a communication session with the LMS.
 */
Scorm.prototype.initialize = function() {
	var result;
    
    API = this.getAPI();
    
    if (API == null){
        this.logError("Could not establish a connection with the LMS.\n\nYour results may not be recorded.");
        return;
    }
	
	switch (this.version) {
		case this.SCORM_1_2:
		default:
			result = API.LMSInitialize("");
			break;
		case this.SCORM_2004:
			result = API.Initialize("");
			break;
	}
    
    if (result == this.SCORM_FALSE){
        var errorCode = this.getLastError();
        var errorString = this.getErrorString(errorCode);
        var diagnostic = this.getDiagnostic(errorCode);
        
        var errorDescription = "Code: " + errorCode + "\nDescription: " + errorString + "\nDiagnostic: " + diagnostic;
        
        this.logError("Could not initialize communication with the LMS.\n\nYour results may not be recorded.\n\n" + errorDescription);
        return false;
    } else {
		this.logInfo("LMS communication initialized with SCORM Version: " + this.version);
	}
	
    this.initialized = true;
	
	return true;
}

/**
 * Ends a communication session with the LMS.
 */
Scorm.prototype.commit = function() {
	var success = false;
    
    if (this.initialized == false || this.finishCalled == true) {
		return;
	}
				
	switch (this.version) {
		case this.SCORM_1_2:
		default:
			success = API.LMSCommit("");
			break;
		case this.SCORM_2004:
			success = API.Commit("");
			break;
	}

	if (!success) {
		this.logError("Couldn't commit.");
	}
}

/**
 * Ends a communication session with the LMS.
 */
Scorm.prototype.finish = function() {
	var result;
    
    if (this.initialized == false || this.finishCalled == true) {
		return;
	}
    
	switch (this.version) {
		case this.SCORM_1_2:
		default:
			result = API.LMSFinish("");
			break;
		case this.SCORM_2004:
			result = API.Terminate("");
			break;
	}
    
    this.finishCalled = true;
    
    if (result == this.SCORM_FALSE) {
        var errorCode = this.getLastError();
        var errorString = this.getErrorString(errorCode);
        var diagnostic = this.getDiagnostic(errorCode);
        
        var errorDescription = "Code: " + errorCode + "\nDescription: " + errorString + "\nDiagnostic: " + diagnostic;
        
        this.logError("Could not terminate communication with the LMS.\n\nYour results may not be recorded.\n\n" + errorDescription);
        return;
    }
}

Scorm.prototype.getValue = function(element) {
    var result;
    
    if (this.initialized == false || this.finishCalled == true) {
		return;
	}
    
	switch (this.version) {
		case this.SCORM_1_2:
		default:
			result = API.LMSGetValue(element);
			break;
		case this.SCORM_2004:
			result = API.GetValue(element);
			break;
	}
	    
    if (result == ""){
        var errorCode = this.getLastError();
		
        if (errorCode != this.SCORM_NO_ERROR){
            var errorString = this.getErrorString(errorCode);
            var diagnostic = this.getDiagnostic(errorCode);
            
            var errorDescription = "Code: " + errorCode + "\nDescription: " + errorString + "\nDiagnostic: " + diagnostic;
            
            this.logError("Could not retrieve a value from the LMS.\n\n" + errorDescription);
            return "";
        }
    }
	
    return result;
}

/**
 * Set a value.
 */
Scorm.prototype.setValue = function(element, value) {
    var result;
    
    if (this.initialized == false || this.finishCalled == true) { 
		return; 
	}
    
	switch (this.version) {
		case this.SCORM_1_2:
		default:
			result = API.LMSSetValue(element, value);
			break;
		case this.SCORM_2004:
			result = API.SetValue(element, value);
			break;
	}
    
    if (result == this.SCORM_FALSE){
        var errorCode = this.getLastError();
        var errorString = this.getErrorString(errorCode);
        var diagnostic = this.getDiagnostic(errorCode);
        
        var errorDescription = "Code: " + errorCode + "\nDescription: " + errorString + "\nDiagnostic: " + diagnostic;
        
        this.logError("Could not store a value in the LMS.\n\nYour results may not be recorded.\n\n" + errorDescription);
        return;
    }
}

/**
 * Get last error.
 */
Scorm.prototype.getLastError = function() {
	var errorCode;	
    
    if (this.initialized == false || this.finishCalled == true) {
		return;
	}
	
	switch (this.version) {
		case this.SCORM_1_2:
		default:
			errorCode = API.LMSGetLastError();
			break;
		case this.SCORM_2004:
			errorCode = API.GetLastError();
			break;
	}
	
	return errorCode;
}

/**
 * Get error string.
 */
Scorm.prototype.getErrorString = function(errorCode) {
	var errorString;
    
    if (this.initialized == false || this.finishCalled == true) {
		return;
	}
	
	switch (this.version) {
		case this.SCORM_1_2:
		default:
			errorString = API.LMSGetErrorString(errorCode);
			break;
		case this.SCORM_2004:
			errorString = API.GetErrorString(errorCode);
			break;
	}
	
	return errorString;
}

/**
 * Get diagnostic.
 */
Scorm.prototype.getDiagnostic = function(errorCode) {
	var info;
    
    if (this.initialized == false || this.finishCalled == true) {
		return;
	}
	
	switch (this.version) {
		case this.SCORM_1_2:
		default:
			info = API.LMSGetDiagnostic(errorCode);
			break;
		case this.SCORM_2004:
			info = API.GetDiagnostic(errorCode);
			break;
	}
	
	return info;
}

/**
 * Logs an info message to the console.
 */
Scorm.prototype.logInfo = function(text) {
	console.log("[SCORM - Info] " + text);
}

/**
 * Logs an error message to the console.
 */
Scorm.prototype.logError = function(text) {
	console.log("[SCORM - Error] " + text);
}

/**
 * Identifies the learner on behalf of whom the SCO was launched.
 */
Scorm.prototype.getLearnerId = function() {
	var learnerId;
	
	switch (this.version) {
		case this.SCORM_1_2:
		default:
			learnerId = this.getValue("cmi.core.student_id");
			break;
		case this.SCORM_2004:
			learnerId = this.getValue("cmi.learner_id");
			break;
	}

	return learnerId;
}

/**
 * Name provided for the learner by the LMS.
 */
Scorm.prototype.getLearnerName = function() {
	var learnerName;
	
	switch (this.version) {
		case this.SCORM_1_2:
		default:
			learnerName = this.getValue("cmi.core.student_name");
			break;
		case this.SCORM_2004:
			learnerName = this.getValue("cmi.learner_name");
			break;
	}

	return learnerName;
}

/**
 * The learner’s current location in the SCO.
 */
Scorm.prototype.getLessonLocation = function() {
	var lessonLocation;
	
	switch (this.version) {
		case this.SCORM_1_2:
		default:
			lessonLocation = this.getValue("cmi.core.lesson_location");
			break;
		case this.SCORM_2004:
			lessonLocation = this.getValue("cmi.location");
			break;
	}

	return lessonLocation;
}

/**
 * The learner’s current location in the SCO.
 */
Scorm.prototype.setLessonLocation = function(location) {
	switch (this.version) {
		case this.SCORM_1_2:
		default:
			this.setValue("cmi.core.lesson_location", location);
			break;
		case this.SCORM_2004:
			this.setValue("cmi.location", location);
			break;
	}

	return true;
}

/**
 * Indicates whether the learner will be credited for performance in the SCO.
 */
Scorm.prototype.getCredit = function() {
	var credit; // credit, no-credit
	
	switch (this.version) {
		case this.SCORM_1_2:
		default:
			credit = this.getValue("cmi.core.credit");
			break;
		case this.SCORM_2004:
			credit = this.getValue("cmi.credit");
			break;
	}

	return credit;
}

/**
 * Indicates whether the learner has completed and satisfied the requirements for the SCO.
 */
Scorm.prototype.getLessonStatus = function() {
	var lessonStatus;
	
	switch (this.version) {
		case this.SCORM_1_2:
		default:
			lessonStatus = this.getValue("cmi.core.lesson_status");
			break;
		case this.SCORM_2004:
			lessonStatus = this.getValue("cmi.completion_status");
			break;
	}

	return lessonStatus;
}

/**
 * Indicates whether the learner has completed and satisfied the requirements for the SCO.
 */
Scorm.prototype.setLessonStatus = function(status) {
	switch (this.version) {
		case this.SCORM_1_2:
		default:
			this.setValue("cmi.core.lesson_status", status);
			break;
		case this.SCORM_2004:
			this.setValue("cmi.completion_status", status);
			break;
	}

	return true;
}

/**
 * Asserts whether the learner has previously accessed the SCO.
 */
Scorm.prototype.getEntry = function() {
	var entry; // ab-initio, resume
	
	switch (this.version) {
		case this.SCORM_1_2:
		default:
			entry = this.getValue("cmi.core.entry");
			break;
		case this.SCORM_2004:
			entry = this.getValue("cmi.entry");
			break;
	}

	return entry;
}

/**
 * Number that reflects the performance of the learner relative to the range bounded by the values of min and max.
 */
Scorm.prototype.getRawScore = function() {
	var rawScore;
	
	switch (this.version) {
		case this.SCORM_1_2:
		default:
			rawScore = this.getValue("cmi.core.score.raw");
			break;
		case this.SCORM_2004:
			rawScore = this.getValue("cmi.score.raw");
			break;
	}

	return rawScore;
}

/**
 * Number that reflects the performance of the learner relative to the range bounded by the values of min and max.
 */
Scorm.prototype.setRawScore = function(score) {
	switch (this.version) {
		case this.SCORM_1_2:
		default:
			this.setValue("cmi.core.score.raw", score);
			break;
		case this.SCORM_2004:
			this.setValue("cmi.score.raw", score);
			break;
	}

	return true;
}

/**
 * Maximum value in the range for the raw score.
 */
Scorm.prototype.getMaxScore = function() {
	var maxScore;
	
	switch (this.version) {
		case this.SCORM_1_2:
		default:
			maxScore = this.getValue("cmi.core.score.max");
			break;
		case this.SCORM_2004:
			maxScore = this.getValue("cmi.score.max");
			break;
	}

	return maxScore;
}

/**
 * Maximum value in the range for the raw score.
 */
Scorm.prototype.setMaxScore = function(score) {
	switch (this.version) {
		case this.SCORM_1_2:
		default:
			this.setValue("cmi.core.score.max", score);
			break;
		case this.SCORM_2004:
			this.setValue("cmi.score.max", score);
			break;
	}

	return true;
}

/**
 * Minimum value in the range for the raw score.
 */
Scorm.prototype.getMinScore = function() {
	var minScore;
	
	switch (this.version) {
		case this.SCORM_1_2:
		default:
			minScore = this.getValue("cmi.core.score.min");
			break;
		case this.SCORM_2004:
			minScore = this.getValue("cmi.score.min");
			break;
	}

	return minScore;
}

/**
 * Minimum value in the range for the raw score.
 */
Scorm.prototype.setMinScore = function(score) {
	switch (this.version) {
		case this.SCORM_1_2:
		default:
			this.setValue("cmi.core.score.min", score);
			break;
		case this.SCORM_2004:
			this.setValue("cmi.score.min", score);
			break;
	}

	return true;
}

/**
 * Sum of all of the learner’s session times accumulated in the current learner attempt.
 */
Scorm.prototype.getTotalTime = function() {
	var totalTime; // in an attempt
	
	switch (this.version) {
		case this.SCORM_1_2:
		default:
			totalTime = this.getValue("cmi.core.total_time");
			break;
		case this.SCORM_2004:
			totalTime = this.getValue("cmi.total_time");
			break;
	}

	return this.fromTimespanToCentiseconds(totalTime);
}

/**
 * Identifies one of three possible modes in which the SCO may be presented to the learner.
 */
Scorm.prototype.getLessonMode = function() {
	var lessonMode;
	
	switch (this.version) {
		case this.SCORM_1_2:
		default:
			lessonMode = this.getValue("cmi.core.lesson_mode");
			break;
		case this.SCORM_2004:
			lessonMode = this.getValue("cmi.mode");
			break;
	}

	return lessonMode;
}

/**
 * Indicates how or why the learner left the SCO
 */
Scorm.prototype.exit = function(reason) {
	switch (this.version) {
		case this.SCORM_1_2:
		default:
			this.setValue("cmi.core.exit", reason);
			break;
		case this.SCORM_2004:
			this.setValue("cmi.exit", reason);
			break;
	}
	
	return true;
}

/**
 * Amount of time that the learner has spent in the current learner session for this SCO.
 */
Scorm.prototype.setSessionTime = function(centiseconds) {	
	switch (this.version) {
		case this.SCORM_1_2:
		default:
			this.setValue("cmi.core.session_time", this.fromCentisecondsToTimespan(centiseconds));
			break;
		case this.SCORM_2004:
			this.setValue("cmi.session_time", this.fromCentisecondsToTimespan(centiseconds));
			break;
	}

	return true;
}

/**
 * Provides space to store and retrieve data between learner sessions.
 */
Scorm.prototype.getSuspendData = function() {	
	var suspendData;
	
	switch (this.version) {
		case this.SCORM_1_2:
		default:
			suspendData = this.getValue("cmi.suspend_data");
			break;
		case this.SCORM_2004:
			suspendData = this.getValue("cmi.suspend_data");
			break;
	}

	return suspendData;
}

/**
 * Provides space to store and retrieve data between learner sessions.
 */
Scorm.prototype.setSuspendData = function(data) {
	switch (this.version) {
		case this.SCORM_1_2:
		default:
			this.setValue("cmi.suspend_data", data);
			break;
		case this.SCORM_2004:
			this.setValue("cmi.suspend_data", data);
			break;
	}

	return true;
}

/**
 * Textual input from the learner about the SCO.
 */
Scorm.prototype.getComments = function() {
	var n;
	
	switch (this.version) {
		case this.SCORM_1_2:
		default:
			n = this.getValue("cmi.comments");
			break;
		case this.SCORM_2004:
			n = this.getValue("cmi.comments_from_learner._children");
			break;
	}

	return n;
}

/**
 * Comments or annotations associated with a SCO.
 */
Scorm.prototype.getCommentsFromLMS = function() {
	var n;
	
	switch (this.version) {
		case this.SCORM_1_2:
		default:
			n = this.getValue("cmi.comments_from_lms");
			break;
		case this.SCORM_2004:
			n = this.getValue("cmi.comments_from_lms._children");
			break;
	}

	return n;
}

/**
 * Get the objectives.
 */
Scorm.prototype.getObjectives = function() {
	var objectives = [];
	
	for (var index = 0; index < this.getValue("cmi.objectives._count"); index++) {
		var objective = {};
		
		switch (this.version) {
			case this.SCORM_1_2:
			default:
				objective.id = this.getValue("cmi.objectives." + index + ".id");
				objective.minScore = this.getValue("cmi.objectives." + index + ".score.min");
				objective.maxScore = this.getValue("cmi.objectives." + index + ".score.max");
				objective.rawScore = this.getValue("cmi.objectives." + index + ".score.raw");
				
				objective.successStatus = this.getValue("cmi.objectives." + index + ".status");
				
				objectives.push(objective);
				break;
			case this.SCORM_2004:
				objective.id = this.getValue("cmi.objectives." + index + ".id");
				objective.minScore = this.getValue("cmi.objectives." + index + ".score.min");
				objective.maxScore = this.getValue("cmi.objectives." + index + ".score.max");
				objective.rawScore = this.getValue("cmi.objectives." + index + ".score.raw");
				
				objective.scaledScore = this.getValue("cmi.objectives." + index + ".score.scaled");
				objective.successStatus = this.getValue("cmi.objectives." + index + ".success_status");
				objective.completionStatus = this.getValue("cmi.objectives." + index + ".completion_status");
				
				objective.progressMeasure = this.getValue("cmi.objectives." + index + ".progress_measure");
				objective.description = this.getValue("cmi.objectives." + index + ".description");
				
				objectives.push(objective);
				break;
		}
	}
	
	return JSON.stringify(objectives);
}

/**
 * Set a specific objective.
 */
Scorm.prototype.setObjective = function(data) {
	var id = data.id.replace(/[^a-z0-9]/gi,'');
	var minScore = data.minScore;
	var maxScore = data.maxScore;
	var rawScore = data.rawScore;
	var scaledScore = data.scaledScore;
	var successStatus = data.successStatus;
	var completionStatus = data.completionStatus;
	var progressMeasure = data.progressMeasure;
	var description = data.description;
	
	var objectiveCount = this.getValue("cmi.objectives._count");
	
	var found;
	
	for (var i = 0; i < objectiveCount; i++) {
		if (this.getValue("cmi.objectives." + i + ".id") == id) {
			found = true;
			
			break;
		}
	}
	
	var index;
	
	if (found) {
		index = i;
	} else {
		index = objectiveCount;
	}
	
	switch (this.version) {
		case this.SCORM_1_2:
		default:
			this.setValue("cmi.objectives." + index + ".id", id);
			this.setValue("cmi.objectives." + index + ".score.min", minScore);
			this.setValue("cmi.objectives." + index + ".score.max", maxScore);
			this.setValue("cmi.objectives." + index + ".score.raw", rawScore);
			
			this.setValue("cmi.objectives." + index + ".status", successStatus);
			break;
		case this.SCORM_2004:
			this.setValue("cmi.objectives." + index + ".id", id);
			this.setValue("cmi.objectives." + index + ".score.min", minScore);
			this.setValue("cmi.objectives." + index + ".score.max", maxScore);
			this.setValue("cmi.objectives." + index + ".score.raw", rawScore);
			
			this.setValue("cmi.objectives." + index + ".score.scaled", scaledScore);
			
			if (successStatus != "passed" && successStatus != "failed" && successStatus != "unknown") {
				this.logError("Invalid 'success_status' value: '" + successStatus + "'. Possible values: passed, failed, unknown.");
			}
			
			this.setValue("cmi.objectives." + index + ".success_status", successStatus);
			
			if (completionStatus != "completed" && completionStatus != "incomplete" && completionStatus != "not attempted" && completionStatus != "unknown") {
				this.logError("Invalid 'completion_status' value: '" + completionStatus + "'. Possible values: passed, failed, unknown.");
			}
			
			this.setValue("cmi.objectives." + index + ".completion_status", completionStatus);
			
			this.setValue("cmi.objectives." + index + ".progress_measure", progressMeasure);
			this.setValue("cmi.objectives." + index + ".description", description);
			break;
	}
	
	return true;
}

/**
 * The learner’s preferred language for SCOs with multilingual capability.
 */
Scorm.prototype.getLanguage = function() {
	var n;
	
	switch (this.version) {
		case this.SCORM_1_2:
		default:
			n = this.getValue("cmi.student_preference.language");
			break;
		case this.SCORM_2004:
			n = this.getValue("cmi.learner_preference.language");
			break;
	}

	return n;
}

/**
 * Converts from centiseconds to Timespan format.
 */
Scorm.prototype.fromCentisecondsToTimespan = function(centiseconds) {
	switch (this.version) {
		case this.SCORM_1_2:
		default:
			return centisecsToSCORM12Duration(centiseconds);
		case this.SCORM_2004:
			return centisecsToISODuration(centiseconds);
	}
}

/**
 * Converts from Timespan format to centiseconds.
 */
Scorm.prototype.fromTimespanToCentiseconds = function(timespan) {
	switch (this.version) {
		case this.SCORM_1_2:
		default:
			return SCORM12DurationToCs(timespan);
		case this.SCORM_2004:
			return ISODurationToCentisec(timespan);
	}
}