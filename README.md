# Pub/Sub Pattern

This is an example for Pub/Sub design pattern implementation in C#. 
For consuming the API you need to provide a model contains Date and Temperature in celsios. 
After calling the post method on API the forecast will transformed and the transport to all media that have registerd in broadcasting service. 
Then each medium will published the forecast in a xml file. 
So far this solution contains 3 media (newspapar, radiostation, socialmedia). 
