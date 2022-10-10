# Qweebi-3D-application-exercise

## Task
<ol>
  <li>Create a project with the following 3D assets imported into the scene: <a href="https://drive.google.com/drive/folders/1r0TRZ1dmDzCgkZA7LrNqZN6TI4_oNZH3">3D Assets</a></li>
  <li>Add the following user interactions:
    <ol>
      <li>Add an orbital camera system that orbits around the axle. Holding down the right mouse button and dragging should control this camera.</li>
      <li>Left clicking and dragging should allow the user to select either of the two imported models in the scene and move them around.</li>
    </ol>
  </li>
  <li>When the wheel and axle are brought near together and the user releases the mouse, the following checks should be made:
    <ol>
      <li>If the end of the axle is within a certain distance from the socket on the wheel (where the axle would naturally fit), the two objects should snap together in a natural fashion (including any object transformations like rotations needed) when the left mouse button is released (object deselected).</li>
      <li>Once the objects snap together, they should be treated as a single object for any future 3D and physics operations.</li>
      <li>If the wheel is not close to the end of the axle when the object is deselected, it should be left as is.</li>
    </ol>
</li>

<hr>
<img src = "Assets\Art\Demo GIFs\Demo - ConMode.gif" />
<hr>

<li>Add a button that allows the user to toggle a “Paint” mode, i.e, it should support the following features while in the Paint mode.
  <ol>
    <li>All object interactions should be disabled (Clicking and dragging).</li>
    <li>Camera controls should still be functional.</li>
    <li>Clicking and dragging in this mode should allow the user to “paint” the 3D objects, i.e
      <ol>
        <li>Clicking and dragging should create a visible trail on the surface of the 3D model. This trail should be in 3D space on the surface of the
model.</li>
        <li>When painting the composite model of the axle and the wheel, the paint should continue naturally across both the axle and wheel as the
user paints, without needing to start a new paint interaction.</li>
        <li>It is NOT necessary to provide a color selection to the user, any colour which will be visible on the surface of the 3D model will suffice.</li>
      </ol>
   </ol>
</li>

<hr>
<img src = "Assets\Art\Demo GIFs\Demo - ColMode.gif" />
<hr>
</ol>
