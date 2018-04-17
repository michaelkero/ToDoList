'usr strict'
var mongoose = require('mongoose');

 var TaskSchema  = new mongoose.Schema({
    name: {
        type: String,
        required: 'Enter the mane of the task'
    },
    createdDate: {
        type: Date,
        default: Date.now
    },
    status: {
        type: [{
            type: String, 
            enum: ['pending', 'ongoing', 'completed']
        }],
        default: ['pending']
    }
});

module.exports = mongoose.model('Tasks', TaskSchema);