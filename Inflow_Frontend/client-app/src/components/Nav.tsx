import React from 'react';
import './Nav.scss'

function Nav() {
    return(
        <div className="nav-component">
            <div className="inflow-label-container">
                <div className="inflow-label">Inflow</div>
            </div>
            <div className="nav-items-container"></div>
            <div className="nav-controls">
                <div className="user-profile" />
            </div>
        </div>
    );
}

export default Nav;