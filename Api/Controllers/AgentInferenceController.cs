using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class AgentInferenceController : Controller
{
    // base
    [HttpPost]
    [Route("/base")]
    public IActionResult Inference()
    {
        return Accepted();
    }
    
    // base + draw
    [HttpPost]
    [Route("/base/draw-to-canvas")]
    public IActionResult InferenceWithDrawToCanvas()
    {
        return Accepted();
    }
    
    // base + share
    [HttpPost]
    [Route("/base/share-canvas")]
    public IActionResult InferenceWithShareCanvas()
    {
        return Accepted();
    }

    
    // base + draw + share
    [HttpPost]
    [Route("/base/draw-to-canvas/share-canvas")]
    public IActionResult InferenceWithDrawToCanvasAndShareCanvas()
    {
        return Accepted();
    }
    
    // search
    [HttpPost]
    [Route("/search")]
    public IActionResult InferenceWithSearch()
    {
        return Accepted();
    }
    
    // search + draw
    [HttpPost]
    [Route("/search/draw-to-canvas")]
    public IActionResult InferenceWithSearchAndDrawToCanvas()
    {
        return Accepted();
    }
    
    // search + share
    [HttpPost]
    [Route("/search/share-canvas")]
    public IActionResult InferenceWithSearchAndShareCanvas()
    {
        return Accepted();
    }
    
    // search + draw + share
    [HttpPost]
    [Route("/search/draw-to-canvas/share-canvas")]
    public IActionResult InferenceWithSearchAndDrawToCanvasAndShareCanvas()
    {
        return Accepted();
    }
}
