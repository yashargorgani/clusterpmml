# clusterpmml
A C# Library for Deploying PMML Clutering Model
This is a library for deploying and scoring Clutering model <a href="http://dmg.org/pmml/pmml-v4-2-1.html">PMML v4.2</a> implemented in <a href="https://www.ibm.com/products/spss-modeler">IBM SPSS Modeler</a> software.
With this library you can implement a model in software, export the PMML file and deploy the model in your own application.
<h3>How to Use</h3>
<p>In your application code, you construct a <code>ClusterModel</code> object with path of proper PMML file:</p>
<code>ClusterModel model = new ClusterModel("E:/K-Means.xml");</code>
<p>Then, you should create a dictionary for your data input:</p>
<code>Dictionary<span><</span>string, object<span>></span> input = new Dictionary<span><</span>string, object<span>></span>();</code></br>
<code>input.Add("value", 42.712);</code></br>
<code>input.Add("pmethod", "CHEQUE");</code></br>
<code>input.Add("sex", "M");</code></br>
<code>input.Add("homeown", "NO");</code></br>
<code>input.Add("income", 27000);</code></br>
<code>input.Add("age", 46);</code></br>
<p> And finally <code>DetectCluster</code> function returns you a dictionay with cluster number as key and distance to that cluster center as value:</p> 
<code>Dictionary<span><</span>string, double<span>></span> cluster = model.DetectCluster(input);</code>
